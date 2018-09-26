using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Effect : EnumBase
    {

        public static Effect Speed = new Effect("minecraft:speed");
        public static Effect Strength = new Effect("minecraft:strength");

        private static IDictionary<string, Effect> Registry;

        public Effect(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Effect>();
            Registry.Add(id, this);
        }

        public static implicit operator Effect(string s)
        {
            return Get(s, Registry);
        }
    }
}
