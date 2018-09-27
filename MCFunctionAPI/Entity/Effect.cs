using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Effect : EnumBase
    {

        public static Effect Speed = new Effect("speed");
        public static Effect Strength = new Effect("strength");
        public static Effect Poison = new Effect("poison");
        public static Effect Regeneration = new Effect("regeneration");

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
