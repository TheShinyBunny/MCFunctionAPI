using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class ConstScore : Score
    {

        private int n;

        public ConstScore(int n)
        {
            this.n = n;
        }

        public override void Set(Objective obj, string field)
        {
            FunctionWriter.Write($"scoreboard players set {field} {obj} {n}");
        }
    }
}
