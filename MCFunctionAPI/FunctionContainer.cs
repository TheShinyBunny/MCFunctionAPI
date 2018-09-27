using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class FunctionContainer : CommandWrapper
    {

        

        public IEnumerable<string> GetRawCommands(Execution execution)
        {
            FunctionWriter.GettingRawCommands = true;
            execution(this);
            return FunctionWriter.GetRawCommands();
        }

        public string GetFirstRawCommand(Execution execution)
        {
            return GetRawCommands(execution).ToList()[0];
        }


        public Execute execute;

        public FunctionContainer()
        {
            execute = new Execute(this);
        }
        

    }
}
