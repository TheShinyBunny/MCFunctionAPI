using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    /// <summary>
    /// The FunctionContainer holds all methods as functions to write into files.
    /// </summary>
    public class FunctionContainer : CommandWrapper
    {
        
        public static IEnumerable<string> GetRawCommands(Action<Entities> execution)
        {
            FunctionWriter.GettingRawCommands = true;
            execution(This);
            return FunctionWriter.GetRawCommands();
        }

        public static string GetFirstRawCommand(Action<Entities> execution)
        {
            return GetRawCommands(execution).ToList()[0];
        }
        
        public static void AddSubFunctions(string path, Type subFolder)
        {
            FunctionWriter.CompileRecursive(subFolder, path + "/" + FunctionWriter.LowerCase(subFolder.Name) + "/");
        }

        public static Execute execute
        {
            get
            {
                return new Execute();
            }
        }

        public static IfStatement If(Entities entities)
        {
            return new IfStatement(entities);
        }

        public class IfStatement
        {

            private Entities entities;

            public IfStatement(Entities entities)
            {
                this.entities = entities;
            }

            public IfStatement Then(Function function)
            {
                execute.If(entities).RunFunction(function);
                return this;
            }

            public IfStatement Then(Action<Entities> action)
            {
                execute.If(entities).RunAll(action);
                return this;
            }

            public void Else(Function function)
            {
                execute.Unless(entities).RunFunction(function);
            }

            public void Else(Action<Entities> action)
            {
                execute.Unless(entities).RunAll(action);
            }

        }

    }
}
