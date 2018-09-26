using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class MaskMode : EnumBase
    {

        public static MaskMode Masked = new MaskMode("masked");
        public static MaskMode Replace = new MaskMode("replace");
        public static FilteredMode Filtered = new FilteredMode();

        private static Dictionary<string, MaskMode> Registry;

        public MaskMode(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, MaskMode>();
            Registry.Add(id, this);
        }

        private MaskMode() : base("")
        {

        }

        public static implicit operator MaskMode(string id)
        {
            if (id.ToLower().StartsWith("filtered"))
            {
                return Filtered.With(Block.Parse(id.Substring(id.IndexOf(' '))));
            }
            return Get(id, Registry);
        }

        public class FilteredMode
        {

            public MaskMode With(Block b)
            {
                return new Filter(b);
            }
        }

        public class Filter : MaskMode
        {

            public Block filter;

            public Filter(Block b)
            {
                this.filter = b;
            }

            public override string ToString()
            {
                return "filtered " + filter;
            }
        }

    }
}
