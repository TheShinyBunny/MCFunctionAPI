using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.BossBar
{
    public class BossStyle : EnumBase
    {

        public static BossStyle Progress = new BossStyle("progress");
        public static BossStyle Notched6 = new BossStyle("notched_6");
        public static BossStyle Notched10 = new BossStyle("notched_10");
        public static BossStyle Notched12 = new BossStyle("notched_12");
        public static BossStyle Notched20 = new BossStyle("notched_20");

        private static Dictionary<string, BossStyle> Registry;

        public BossStyle(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, BossStyle>();
            Registry.Add(id, this);
        }

        public static implicit operator BossStyle(string id)
        {
            return Get(id, Registry);
        }

    }
}
