using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Gamemode : EnumBase
    {
        public static Gamemode Survival = new Gamemode("survival");
        public static Gamemode Creative = new Gamemode("creative");
        public static Gamemode Spectator = new Gamemode("spectator");
        public static Gamemode Adventure = new Gamemode("adventure");

        private static IDictionary<string, Gamemode> Registry;

        public Gamemode(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Gamemode>();
            Registry.Add(id, this);
        }

        public static implicit operator Gamemode(uint index)
        {
            switch (index)
            {
                case 0:
                    return Survival;
                case 1:
                    return Creative;
                case 2:
                    return Adventure;
                case 3:
                    return Spectator;
                default:
                    throw new ArgumentException("Invalid gamemode id '" + index + "'!");
            }
        }

        public static implicit operator Gamemode(string id)
        {
            return Get(id, Registry);
        }
    }
}
