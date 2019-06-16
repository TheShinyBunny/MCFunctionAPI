using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class Objective
    {

        public string Name { get; }

        protected Objective(string name)
        {
            this.Name = name;
        }

        public static Objective Of(string name)
        {
            return new Objective(name);
        }

        public static implicit operator Objective(string s)
        {
            return Of(s);
        }

        public static ScoreRange operator <(Objective o, int num)
        {
            return new ScoreRange(o, new IntRange(null, num - 1));
        }

        public static ScoreRange operator >(Objective o, int num)
        {
            return new ScoreRange(o, new IntRange(num + 1, null));
        }

        public void Create(string criterion)
        {
            FunctionWriter.Write($"scoreboard objectives add {this} {criterion}");
        }

        public void Create(string criterion, TextComponent displayName)
        {
            FunctionWriter.Write($"scoreboard objectives add {this} {criterion} {displayName}");
        }

        public void Dummy()
        {
            Create("dummy");
        }

        public void Remove()
        {
            FunctionWriter.Write("scoreboard objectives remove " + this);
        }

        public override string ToString()
        {
            return Name;
        }

        public Score this[string field]
        {
            get
            {
                return new SourcedScore(this, field);
            }
            set
            {
                value.Set(this, field);
            }
        }

        public Score Everyone
        {
            set
            {
                value.Set(this, "*");
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
            return obj is Objective && (obj as Objective).Name.Equals(Name);
        }

    }
}
