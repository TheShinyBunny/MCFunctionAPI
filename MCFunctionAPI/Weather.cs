using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Weather : EnumBase
    {

        public static Weather Clear = new Weather("clear");
        public static Weather Rain = new Weather("rain");
        public static Weather Thunder = new Weather("thunder");

        private static Dictionary<string, Weather> Registry;

        public Weather(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Weather>();
            Registry.Add(id, this);
        }

        public static implicit operator Weather(string id)
        {
            return Get(id, Registry);
        }

    }
}
