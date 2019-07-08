using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class Criteria
    {

        public static string Mine(string block)
        {
            return "minecraft.mined:minecraft." + block; 
        }

    }
}
