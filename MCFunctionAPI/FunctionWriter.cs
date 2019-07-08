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
    /// The main system for executing function methods and writing them to files
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
        /// The current function writing to.
        /// </summary>
        private static MCFunction Function;
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
            Console.WriteLine($"[{Function.Id}] {cmd}");
            if (GettingRawCommands)
            {
                RawLines.Add(cmd);
            }
            else
            {
                Function.AddLine(cmd);
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
            Console.WriteLine("compiling " + Namespace + ": " + path + " in type " + c);
            object instance = Activator.CreateInstance(c);
            foreach (var f in c.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                if (typeof(Objective) == f.FieldType)
                {
                    Objective value = f.GetValue(instance) as Objective;
                    if (value == null)
                    {
                        value = Objective.Of(f.Name);
                        f.SetValue(instance, value);
                    }
                    Namespace.AddLoadObjective(value,f.GetCustomAttribute<Criterion>()?.Name);
                }
            }
            
            foreach (var m in c.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                if (m.DeclaringType == c || (m.IsVirtual && typeof(FunctionContainer).IsAssignableFrom(m.DeclaringType)))
                {
                    if (m.GetParameters().Count() == 0 || m.GetCustomAttribute<Expand>() != null)
                    {
                        Compile(instance, m, path);
                    }
                }
            }

            foreach (var t in c.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                CompileRecursive(t,path + Utils.LowerCase(t.Name) + "/");
            }
        }

        public static void Compile(object instance, MethodInfo m, string path)
        {
            Console.WriteLine("Compiling method " + m.DeclaringType + " " + m);
            Expand e = m.GetCustomAttribute<Expand>();
            if (e != null)
            {
                foreach (string s in e.All)
                {
                    Function = new MCFunction(new ResourceLocation(Namespace, path + Utils.LowerCase(m.Name) + "/" + s));
                    if (m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string))
                    {
                        m.Invoke(null, new object[] { s });

                    } else
                    {
                        m.Invoke(null, null);
                    }
                    
                    Function.WriteFile();
                    Execute.Clear();
                }
                return;
            }
            ResourceLocation id = new ResourceLocation(Namespace, path + Utils.LowerCase(m.Name));
            Function = new MCFunction(id);
            if (m.GetCustomAttribute<Tick>() != null)
            {
                if (Namespace.TickFunction == null)
                {
                    Namespace.Datapack.CreateTickTag(id);
                    Namespace.TickFunction = Function;
                }
                Function = Namespace.TickFunction;

                foreach (KeyValuePair<ScoreEventHandler, MCFunction> h in Namespace.PendingScoreHandlers)
                {
                    Function.AddScoreTick(h);
                }
            }
            if (m.GetCustomAttribute<Load>() != null)
            {
                if (Namespace.LoadFunction == null)
                {
                    Namespace.Datapack.CreateLoadTag(id);
                    Namespace.LoadFunction = Function;
                }
                Function = Namespace.LoadFunction;


                foreach (KeyValuePair<ScoreEventHandler, MCFunction> h in Namespace.PendingScoreHandlers)
                {
                    Function.AddScoreCreation(h);
                }

            }
            ScoreEventHandler scoreHandler = m.GetCustomAttribute<ScoreEventHandler>();
            ScoreTrigger trigger = m.GetCustomAttribute<ScoreTrigger>();
            if (trigger != null)
            {
                scoreHandler = new ScoreEventHandler(trigger.Objective ?? Utils.LowerCase(m.Name), "trigger", trigger.Range);
            }
            if (scoreHandler != null)
            {
                if (Namespace.LoadFunction == null)
                {
                    Namespace.PendingScoreHandlers.Add(scoreHandler, Function);
                }
                else
                {
                    AppendObjectiveCreation(scoreHandler, Function);
                }

                if (Namespace.TickFunction == null)
                {
                    Namespace.PendingScoreHandlers.Add(scoreHandler, Function);
                }
                else
                {
                    AppendObjectiveTick(scoreHandler, Function);
                }

            }
            
            
            Desc d = m.GetCustomAttribute<Desc>();
            Write("#################################################");
            Write($"# Created on {DateTime.Now.ToShortDateString()}");
            if (d != null)
            {
                if (d.Caller != null)
                {
                    Write("# Called by: " + d.Caller);
                }
                Write("# Description: " + d.Value);
            }
            Write("#################################################");
            Write("");

            object returnValue = m.Invoke(instance, null);

            GiveItem give = m.GetCustomAttribute<GiveItem>();
            if (give != null && returnValue != null)
            {
                if (returnValue is Item)
                {
                    Function.AddLine("give @s " + returnValue);
                } else if (returnValue is IEnumerable<Item> items)
                {
                    if (give.Container == null)
                    {
                        foreach (var i in items)
                        {
                            Function.AddLine("give @s " + i);
                        }
                    } else
                    {
                        Function.AddLine("give @s " + Item.Of(give.Container).SetBlockContainerItems(items));
                    }
                }
            }

            Function.WriteFile();

            Execute.Clear();
        }

        private static void AppendObjectiveTick(ScoreEventHandler h, MCFunction func)
        {
            Namespace.TickFunction.AddScoreTick(new KeyValuePair<ScoreEventHandler, MCFunction>(h, func));
        }

        private static void AppendObjectiveCreation(ScoreEventHandler h, MCFunction func)
        {
            Namespace.LoadFunction.AddScoreCreation(new KeyValuePair<ScoreEventHandler, MCFunction>(h, func));
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

        public static ResourceLocation GetFunctionPath(Namespace ns, CommandWrapper.Function f)
        {
            return GetFunctionPath(ns, f, f.Method.DeclaringType);
        }

        public static ResourceLocation GetFunctionPath(Namespace ns, CommandWrapper.ParameterFunction pf, string param)
        {
            return GetFunctionPath(ns, pf, pf.Method.DeclaringType, param);
        }

        public static ResourceLocation GetFunctionPath(Namespace ns, Delegate f, Type directImplementator, string param = null)
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
            if (f.Method.GetCustomAttribute<Expand>() != null && param != null)
            {
                path += "/" + param;
            }
            return new ResourceLocation(ns, path);
        }

        public static void Space()
        {
            Write0("");
        }

        public static Objective EnsureAgeObjective()
        {
            Objective o = "_age";
            Namespace.AddLoadObjective(o,null);
            return o;
        }
    }
}
