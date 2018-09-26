using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class SetMode : EnumBase
    {

        public static SetMode Keep = new SetMode("keep");
        public static SetMode Hollow = new SetMode("replace");
        public static SetMode Outline = new SetMode("destroy");

        private static Dictionary<string, SetMode> Registry;

        public SetMode(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, SetMode>();
            Registry.Add(id, this);
        }
        
        public static implicit operator SetMode(string id)
        {
            return Get(id, Registry);
        }

    }
}
