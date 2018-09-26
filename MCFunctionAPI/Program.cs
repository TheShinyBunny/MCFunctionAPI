using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    class Program : Datapack
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Namespace main = p.CreateNamespace("main");
            main.CreateFunctions(new MyFunctions());
        }

        public override string GetDescription()
        {
            return "Hello World";
        }

        public override string GetName()
        {
            return "test";
        }
    }
}
