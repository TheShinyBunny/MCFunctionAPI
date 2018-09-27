using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Scoreboard
{
    public class ObjectiveBoolean
    {

        public static readonly Objective BOOL_VALUES = "_boolvalues";
        
        public static void Setup()
        {
            BOOL_VALUES.Create("dummy");
            BOOL_VALUES["true"] = 1;
            BOOL_VALUES["false"] = 0;
        }

        public bool Value {
            set
            {
                FunctionWriter.Write($"scoreboard players set {DEFAULT_PLAYER} {this} {(value ? 1 : 0)}");
            }
        }
        private string Name;

        public ObjectiveBoolean(string Name)
        {
            this.Name = Name;
        }

        public const string DEFAULT_PLAYER = "boolean";

        public void Create()
        {
            FunctionWriter.Write($"scoreboard objectives add {this} dummy");
        }

        public void Remove()
        {
            FunctionWriter.Write($"scoreboard objectives remove {this}");
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator ObjectiveBoolean(string v)
        {
            return new ObjectiveBoolean(v);
        }
    }
}
