using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class ScoreOperator : EnumBase
    {

        public static ScoreOperator Less = new ScoreOperator("<");
        public static ScoreOperator LessOrEqual = new ScoreOperator("<=");
        public static ScoreOperator Equal = new ScoreOperator("=");
        public static ScoreOperator GreaterOrEqual = new ScoreOperator(">=");
        public static ScoreOperator Greater = new ScoreOperator(">");

        private static Dictionary<string, ScoreOperator> Registry;

        public ScoreOperator(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, ScoreOperator>();
            Registry.Add(id, this);
        }

        public static implicit operator ScoreOperator(string s)
        {
            return Get(s, Registry);
        }
    }
}
