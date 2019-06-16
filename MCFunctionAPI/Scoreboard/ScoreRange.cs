using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class ScoreRange
    {

        public Objective obj;
        public IntRange range;

        public ScoreRange(Objective obj, IntRange range)
        {
            this.obj = obj;
            this.range = range;
        }
    }
}
