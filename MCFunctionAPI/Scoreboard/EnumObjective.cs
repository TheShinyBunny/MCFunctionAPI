using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class EnumObjective<T> : Objective where T : EnumBase
    {

        private T[] values;

        protected EnumObjective(string name) : base(name)
        {
            values = (from f in typeof(T).GetFields()
                      where f.FieldType == typeof(T)
                      select (T)f.GetValue(null)).ToArray();
        }

        public new static EnumObjective<T> Of(string name)
        {
            return new EnumObjective<T>(name);
        }

        public static implicit operator EnumObjective<T>(string s)
        {
            return Of(s);
        }

        public new EnumScore<T> this[string field]
        {
            get
            {
                return new EnumScore<T>(this, field);
            }
            set
            {
                if (value.Value != null)
                {
                    FunctionWriter.Write($"scoreboard players set {field} {this} {IndexOf(value)}");
                } else if (!value.Target.Equals(field) || !value.Objective.Equals(this))
                {
                    FunctionWriter.Write($"scoreboard players operation {field} {this} = {value.Target} {value.Objective}");
                }
            }
        }

        public int IndexOf(T val)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == val)
                {
                    return i;
                }
            }
            return -1;
        }

    }

    public class EnumScore<T> where T : EnumBase
    {
        public EnumObjective<T> Objective { get; }
        public string Target { get; }
        public T Value { get; }

        public EnumScore(EnumObjective<T> objective, string target)
        {
            Objective = objective;
            Target = target;
        }
        
        private EnumScore(T value)
        {
            Value = value;
        }

        public Execute IfEquals(T value)
        {
            return new Execute().If(Target, Objective, Objective.IndexOf(value));
        }

        public void ExecuteIfEqualsAny(Action<Entities> runExecute, params T[] values)
        {
            foreach (T t in values)
            {
                IfEquals(t).RunAll(runExecute);
            }
        }

        public static implicit operator EnumScore<T>(T value)
        {
            return new EnumScore<T>(value);
        }

        public static implicit operator T(EnumScore<T> score)
        {
            return score.Value;
        }

        public ResultCommand Get()
        {
            return new ResultCommand($"scoreboard players get {Target} {Objective}");
        }
    }
}
