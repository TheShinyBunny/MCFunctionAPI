using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Difficulty : EnumBase
    {

        public static Difficulty Peaceful = new Difficulty("peaceful");
        public static Difficulty Easy = new Difficulty("easy");
        public static Difficulty Normal = new Difficulty("normal");
        public static Difficulty Hard = new Difficulty("hard");

        private static Dictionary<string, Difficulty> Registry;

        public Difficulty(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Difficulty>();
            Registry.Add(id, this);
        }

        public static implicit operator Difficulty(string id)
        {
            return Get(id, Registry);
        }

        public static implicit operator Difficulty(int index)
        {
            switch (index)
            {
                case 0:
                    return Peaceful;
                case 1:
                    return Easy;
                case 2:
                    return Normal;
                case 3:
                    return Hard;
                default:
                    return null;
            }
        }

    }
}
