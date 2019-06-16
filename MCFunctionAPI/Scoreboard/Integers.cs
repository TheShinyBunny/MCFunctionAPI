using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    
    /// <summary>
    /// A helper class that generates all integers from 1 to a number!
    /// </summary>
    public class Integers
    {
        private static Objective IntObjective;

        public static void Create(int from, int to)
        {
            IntObjective = "__integers";
            IntObjective.Create("dummy");

            for(int i = from; i < to; i++)
            {
                IntObjective[i.ToString()] = i;
            }
        }
    }
}
