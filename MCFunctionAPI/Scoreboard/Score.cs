using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public abstract class Score
    {

        protected string target;
        protected Objective objective;

        protected Score()
        {

        }

        public abstract void Set(Objective obj, string field);

        public static Score operator ++(Score s)
        {
            FunctionWriter.Write($"scoreboard players add {s.target} {s.objective} 1");
            return s;
        }

        public static Score operator --(Score s)
        {
            FunctionWriter.Write($"scoreboard players remove {s.target} {s.objective} 1");
            return s;
        }

        public static Score operator +(Score s, int num)
        {
            FunctionWriter.Write($"scoreboard players add {s.target} {s.objective} {num}");
            return s;
        }

        public static Score operator -(Score s, int num)
        {
            FunctionWriter.Write($"scoreboard players remove {s.target} {s.objective} {num}");
            return s;
        }

        internal bool HasValue()
        {
            return objective == null;
        }

        public static Score operator +(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} += {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator -(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} -= {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator *(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} *= {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator /(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} /= {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator %(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} %= {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator <(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} < {s2.target} {s2.objective}");
            return s;
        }

        public static Score operator >(Score s, Score s2)
        {
            FunctionWriter.Write($"scoreboard players operation {s.target} {s.objective} > {s2.target} {s2.objective}");
            return s;
        }
        
        public void Swap(Score other)
        {
            FunctionWriter.Write($"scoreboard players operation {target} {objective} >< {other.target} {other.objective}");
        }

        public void Assign(Score to)
        {
            FunctionWriter.Write($"scoreboard players operation {target} {objective} = {to.target} {to.objective}");
        }

        public static implicit operator Score(int n)
        {
            return new ConstScore(n);
        }

        public static implicit operator Score(ResultCommand cmd)
        {
            return new QueriedScore(cmd);
        }

        public void Get()
        {
            FunctionWriter.Write($"scoreboard players get {target} {objective}");
        }

        public Objective GetObjective()
        {
            return objective;
        }

        public string GetTarget()
        {
            return target;
        }

        public Execute IsInRange(IntRange range)
        {
            return new Execute().If(target, objective, range);
        }

    }
}
