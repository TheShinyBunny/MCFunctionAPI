using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MCFunctionAPI.FunctionContainer;

namespace MCFunctionAPI
{
    public class FunctionWriter
    {
        public static Execute Execute;
        public static Namespace Namespace;
        private static List<string> Lines = new List<string>();
        private static string CurrentPath;
        public static bool GettingRawCommands;
        private static List<string> RawLines = new List<string>();

        public static void Write(string cmd)
        {
            if (Execute != null)
            {
                string executeCmd = Execute.ToString() + "run " + cmd;
                Execute.Reset();
                Execute = null;
                Write(executeCmd);
                return;
            }
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

        public static void GenerateFunctions(Namespace ns, FunctionContainer container)
        {
            Namespace = ns;
            Directory.CreateDirectory(ns.Path + "/functions");
            CompileRecursive(container,"/");
        }

        public static void CompileRecursive(FunctionContainer c, string path)
        {
            c.Namespace = Namespace;
            Console.WriteLine("compiling " + Namespace + ": " + path);
            foreach (var m in c.GetType().GetMethods())
            {
                if (m.DeclaringType == c.GetType())
                {
                    Compile(c, m, path);
                }
            }

            foreach (var t in c.GetType().GetNestedTypes())
            {
                if (typeof(FunctionContainer).IsAssignableFrom(t))
                {
                    FunctionContainer sc = (FunctionContainer)Activator.CreateInstance(t);
                    CompileRecursive(sc,path + t.Name.ToLower() + "/");
                }
            }
        }

        public static void Compile(FunctionContainer c, MethodInfo m, string path)
        {
            Lines.Clear();
            CurrentPath = path + m.Name.ToLower();
            m.Invoke(c,null);
            Directory.CreateDirectory(Namespace.Path + "/functions" + path);
            File.WriteAllLines(Namespace.Path + "/functions" + path + m.Name.ToLower() + ".mcfunction",Lines);
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
    }
}
