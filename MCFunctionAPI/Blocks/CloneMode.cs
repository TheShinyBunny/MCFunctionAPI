using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class CloneMode : EnumBase
    {

        public static CloneMode Normal = new CloneMode("normal");
        public static CloneMode Move = new CloneMode("move");
        public static CloneMode Force = new CloneMode("force");

        private static Dictionary<string, CloneMode> Registry;

        public CloneMode(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, CloneMode>();
            Registry.Add(id, this);
        }

        public static implicit operator CloneMode(string id)
        {
            return Get(id, Registry);
        }

    }
}
