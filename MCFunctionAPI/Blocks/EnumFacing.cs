using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class EnumFacing : EnumBase
    {

        public static EnumFacing West = new EnumFacing("west");
        public static EnumFacing East = new EnumFacing("east");
        public static EnumFacing North = new EnumFacing("north");
        public static EnumFacing South = new EnumFacing("south");
        public static EnumFacing Up = new EnumFacing("up");
        public static EnumFacing Down = new EnumFacing("down");

        private static IDictionary<string, EnumFacing> Registry;

        public EnumFacing(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, EnumFacing>();
            Registry.Add(id, this);
        }

        public static implicit operator EnumFacing(string s)
        {
            return Get(s,Registry);
        }
        
        public static implicit operator State(EnumFacing facing)
        {
            return facing.id;
        }
    }
}
