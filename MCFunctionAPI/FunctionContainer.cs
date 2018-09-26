using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class FunctionContainer : CommandWrapper
    {

        public delegate void Function();

        public Execute execute = new Execute();

        private Namespace ns;
        public Namespace Namespace
        {
            get => ns;
            set
            {
                if (ns == null)
                {
                    ns = value;
                }
            }
        }


        public void WriteRaw(string cmd)
        {
            FunctionWriter.Write(cmd);
        }

        public void Run(Function func)
        {
            if (typeof(FunctionContainer).IsAssignableFrom(func.Target.GetType()))
            {
                FunctionContainer container = (FunctionContainer)func.Target;
                Type declarer = func.Method.DeclaringType;
                Run(Namespace, declarer, func.Method.Name);
            }
        }

        public void Run(string path)
        {
            if (path.Contains(":"))
            {
                FunctionWriter.Write("function " + path);
            }
            else
            {
                FunctionWriter.Write("function " + Namespace + ":" + path);
            }
        }

        public void Run(Namespace ns, Type subFolder, string methodName)
        {
            string path = "";
            while (subFolder.DeclaringType != null)
            {
                subFolder = subFolder.DeclaringType;
                path = subFolder.Name.ToLower() + "/" + path;
            }
            path += methodName.ToLower();
            FunctionWriter.Write("function " + ns + ":" + path);
        }

    }
}
