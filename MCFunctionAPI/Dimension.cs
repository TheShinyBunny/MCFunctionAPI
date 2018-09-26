using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Dimension : EnumBase
    {

        public static Dimension Overworld = new Dimension("overworld");
        public static Dimension Nether = new Dimension("the_nether");
        public static Dimension End = new Dimension("the_end");

        private static Dictionary<string, Dimension> Registry;

        public Dimension(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Dimension>();
            Registry.Add(id, this);
        }

        public static implicit operator Dimension(string id)
        {
            return Get(id, Registry);
        }

    }
}
