using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class FillMode : EnumBase
    {
        public static FillMode Destroy = new FillMode("destroy");
        public static ReplaceMode Replace = new ReplaceMode();
        public static FillMode Keep = new FillMode("keep");
        public static FillMode Hollow = new FillMode("hollow");
        public static FillMode Outline = new FillMode("outline");

        private static Dictionary<string, FillMode> Registry;

        public FillMode(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, FillMode>();
            Registry.Add(id, this);
        }

        private FillMode() : base("")
        {

        }

        public static implicit operator FillMode(string id)
        {
            if (id.ToLower().StartsWith("replace"))
            {
                return Replace.All(Block.Parse(id.Substring(id.IndexOf(' '))));
            }
            return Get(id, Registry);
        }

        public class ReplaceMode
        {

            public FillMode All(Block b)
            {
                return new Replacement(b);
            }
        }

        public class Replacement : FillMode
        {

            public Block replace;

            public Replacement(Block b)
            {
                this.replace = b;
            }

            public override string ToString()
            {
                return "replace " + replace;
            }
        }
    }
}
