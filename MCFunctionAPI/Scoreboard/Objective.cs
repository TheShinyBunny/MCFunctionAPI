using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class Objective
    {

        private string name;

        private Objective(string name)
        {
            this.name = name;
        }

        public static Objective Of(string name)
        {
            return new Objective(name);
        }

        public static implicit operator Objective(string s)
        {
            return Of(s);
        }

        public void Create(string criterion)
        {
            FunctionWriter.Write($"scoreboard objectives add {this} {criterion}");
        }

        public void Create(string criterion, TextComponent displayName)
        {
            FunctionWriter.Write($"scoreboard objectives add {this} {criterion} {displayName}");
        }

        public void Remove()
        {
            FunctionWriter.Write("scoreboard objectives remove " + this);
        }

        public override string ToString()
        {
            return name;
        }

        public Score this[string field]
        {
            get { return new Score(this, field); }
            set
            {
                if (value.HasValue())
                {
                    FunctionWriter.Write($"scoreboard players set {field} {this} {value}");
                }
                else if (!value.GetTarget().Equals(field) || !value.GetObjective().Equals(this))
                {
                    FunctionWriter.Write($"scoreboard players operation {field} {this} = {value.GetTarget()} {value.GetObjective()}");
                }
            }
        }

        public static void ResetAll(string name)
        {
            FunctionWriter.Write("scoreboard players reset " + name);
        }

        public void Enable(string name)
        {
            FunctionWriter.Write($"scoreboard players enable {name} {this}");
        }

        public override bool Equals(object obj)
        {
            return obj is Objective && (obj as Objective).name.Equals(name);
        }

    }
}
