using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class SourcedScore : Score
    {
        
        public SourcedScore(Objective o, string target)
        {
            this.objective = o;
            this.target = target;
        }

        public override void Set(Objective obj, string field)
        {
            if (this.objective != obj && this.target != field)
            {
                FunctionWriter.Write($"scoreboard players operation {this.target} {this.objective} = {field} {obj}");
            }
        }
    }
}
