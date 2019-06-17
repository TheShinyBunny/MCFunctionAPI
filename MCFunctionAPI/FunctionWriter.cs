using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{

    /// <summary>
    /// The main system for writing function files
    /// </summary>
    public class FunctionWriter
    {
        /// <summary>
        /// A stack that holds the current nested execute commands
        /// </summary>
        public static Stack<Execute> Execute = new Stack<Execute>();
        /// <summary>
        /// The current namespace the function writer is adding functions to
        /// </summary>
        public static Namespace Namespace;
        /// <summary>
        /// The lines generated to write into the .mcfunction file
        /// </summary>
        private static List<string> Lines = new List<string>();
        /// <summary>
        /// The currently compiling function's path, including the file name (without extension)
        /// </summary>
        private static string CurrentPath;
        /// <summary>
        /// If true, every new line written to the function container will be inserted to the <see cref="RawLines"/>, instead of <see cref="Lines"/>.
        /// </summary>
        public static bool GettingRawCommands;
        /// <summary>
        /// All collected lines while <see cref="GettingRawCommands"/> is true. Used to get commands generated from the API without adding them to the file.
        /// </summary>
        private static List<string> RawLines = new List<string>();

        /// <summary>
        /// Writes a single line to the mcfunction file. Will add to the line any existing nested execute commands.
        /// </summary>
        /// <param name="cmd">The line to add to the <see cref="Lines"/> list</param>
        public static void Write(string cmd)
        {
            if (Execute.Count > 0)
            {
                string execute = Execute.Peek().ToString();
                if (cmd.StartsWith("execute "))
                    cmd = cmd.Substring("execute ".Length);
                else
                    execute += "run ";
                string executeCmd = execute + cmd;
                Execute e = Execute.Pop();
                Write(executeCmd);
                if (e.UseOnce)
                {
                    return;
                }
                else
                {
                    Execute.Push(e);
                }
            } else
            {
                Write0(cmd);
            }
        }

        private static void Write0(string cmd)
        {
            Console.WriteLine($"[{CurrentPath}] {cmd}");
            if (GettingRawCommands)
            {
                RawLines.Add(cmd);
            }
            else
            {
                Lines.Add(cmd);
            }
        }

        /// <summary>
        /// Generates all functions inside the given <seealso cref="FunctionContainer"/>
        /// </summary>
        /// <param name="path">The current path to the functions inside the container</param>
        /// <param name="ns">The namespace to add functions to</param>
        /// <param name="container">A <code>typeof(Type : FunctionContainer) to make all static methods to functions</code></param>
        public static void GenerateFunctions(string path, Namespace ns, Type container)
        {
            Namespace = ns;
            Directory.CreateDirectory(ns.Path + "/functions");
            CompileRecursive(container,path);
        }

        public static void CompileRecursive(Type c, string path)
        {
            Console.WriteLine("compiling " + Namespace + ": " + path);
            foreach (var m in c.GetMethods())
            {
                if ((m.DeclaringType == c || m.IsVirtual))
                {
                    if (m.GetParameters().Count() == 0 && m.ReturnType == typeof(void))
                    {
                        Compile(c, m, path);
                    }
                }
            }

            foreach (var t in c.GetNestedTypes())
            {
                CompileRecursive(t,path + Utils.LowerCase(t.Name) + "/");
            }
        }

        public static void Compile(Type c, MethodInfo m, string path)
        {
            CurrentPath = path + Utils.LowerCase(m.Name);
            Lines.Clear();
            Desc d = m.GetCustomAttribute<Desc>();
            if (d != null)
            {
                Write("#################################################");
                Write($"# Created on {DateTime.Now.ToShortDateString()}");
                if (d.Caller != null)
                {
                    Write("# Called by: " + d.Caller);
                }
                Write("# Description: " + d.Value);
                Write("#################################################");
                Write("");
            }
            object instance = Activator.CreateInstance(c);
            foreach (FieldInfo f in c.GetFields())
            {
                if (typeof(Objective).IsAssignableFrom(f.FieldType))
                {
                    Namespace.AddLoadObjective((Objective)f.GetValue(instance));
                }
            }
            
            m.Invoke(instance, null);
            if (path != "")
            {
                Directory.CreateDirectory(Namespace.Path + "/functions/" + path);
            }
            File.WriteAllLines(Namespace.Path + "/functions/" + CurrentPath + ".mcfunction",Lines);
            ResourceLocation id = new ResourceLocation(Namespace, CurrentPath);
            Execute.Clear();
            if (m.GetCustomAttribute<Tick>() != null && Namespace.TickFunctionPath == null)
            {
                Namespace.Datapack.CreateTickTag(id);
                Namespace.TickFunctionPath = CurrentPath;
                foreach (KeyValuePair<ScoreEventHandler, string> h in Namespace.PendingScoreHandlers)
                {
                    AppendObjectiveTick(h.Key, h.Value, CurrentPath);
                }
                
                if (Namespace.LoadFunctionPath != null)
                {
                    Namespace.PendingScoreHandlers.Clear();
                }
            }
            if (m.GetCustomAttribute<Load>() != null && Namespace.LoadFunctionPath == null)
            {
                Namespace.Datapack.CreateLoadTag(id);
                Namespace.LoadFunctionPath = CurrentPath;
                foreach (KeyValuePair<ScoreEventHandler,string> h in Namespace.PendingScoreHandlers)
                {
                    AppendObjectiveCreation(h.Key, CurrentPath);
                }
                foreach (Objective o in Namespace.LoadObjectives)
                {
                    AppendObjectiveCreation(new ScoreEventHandler(o.Name, "dummy"), CurrentPath);
                }

                if (Namespace.TickFunctionPath != null)
                {
                    Namespace.PendingScoreHandlers.Clear();
                }
            }
            ScoreEventHandler scoreHandler = m.GetCustomAttribute<ScoreEventHandler>();
            if (scoreHandler != null)
            {
                if (Namespace.LoadFunctionPath == null || Namespace.TickFunctionPath == null)
                {
                    Namespace.PendingScoreHandlers.Add(scoreHandler, CurrentPath);
                }

                if (Namespace.LoadFunctionPath != null)
                {
                    AppendObjectiveCreation(scoreHandler, Namespace.LoadFunctionPath);
                }
                if (Namespace.TickFunctionPath != null)
                {
                    AppendObjectiveTick(scoreHandler, CurrentPath, Namespace.TickFunctionPath);
                }
                
            }
        }

        private static void AppendObjectiveTick(ScoreEventHandler h, string funcPath, string filePath)
        {
            File.AppendAllLines(Namespace.Path + "/functions/" + filePath + ".mcfunction", new string[] { $"execute as @e[scores={{{h.Objective}={h.TargetValue}}}] at @s run function {Namespace}:{funcPath}" });
        }

        private static void AppendObjectiveCreation(ScoreEventHandler h, string path)
        {
            File.AppendAllLines(Namespace.Path + "/functions/" + path + ".mcfunction", new string[] { $"scoreboard objectives add {h.Objective} {h.Criteria}" });
        }

        public static IEnumerable<string> GetRawCommands()
        {
            GettingRawCommands = false;
            List<string> list = new List<string>(RawLines);
            RawLines.Clear();
            foreach (var i in list)
            {
                yield return i;
            }
        }

        public static string GetFunctionPath(CommandWrapper.Function f)
        {
            return GetFunctionPath(f, f.Method.DeclaringType);
        }

        public static string GetFunctionPath(CommandWrapper.Function f, Type directImplementator)
        {
            string path = "";
            Type subFolder = directImplementator;
            if (subFolder.GetCustomAttribute<Root>() == null)
            {
                while (subFolder != null)
                {
                    path = Utils.LowerCase(subFolder.Name) + "/" + path;
                    if (subFolder.DeclaringType == null)
                    {
                        NestedFolder nested = subFolder.GetCustomAttribute<NestedFolder>();
                        if (nested == null)
                        {
                            subFolder = null;
                        }
                        else
                        {
                            subFolder = nested.SuperType;
                        }
                    }
                    else
                    {
                        subFolder = subFolder.DeclaringType;
                    }
                }
            }
            path += Utils.LowerCase(f.Method.Name);
            return Namespace + ":" + path;
        }

        public static void Space()
        {
            Write0("");
        }
    }
}
