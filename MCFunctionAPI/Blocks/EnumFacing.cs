using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class EnumFacing : EnumBase
    {

        public static EnumFacing South = new EnumFacing("south");
        public static EnumFacing West = new EnumFacing("west");
        public static EnumFacing North = new EnumFacing("north");
        public static EnumFacing East = new EnumFacing("east");
        public static EnumFacing Up = new EnumFacing("up");
        public static EnumFacing Down = new EnumFacing("down");

        private static IDictionary<string, EnumFacing> Registry;

        public static IEnumerable<EnumFacing> All
        {
            get
            {
                return Registry.Values;
            }
        }

        public float Rotation
        {
            get
            {
                switch(Id)
                {
                    case "south":
                        return 0;
                    case "west":
                        return 90;
                    case "north":
                        return 180;
                    case "east":
                        return -90;
                }
                return 0;
            }
        }

        public EnumFacing(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, EnumFacing>();
            Registry.Add(id, this);
        }

        public static implicit operator EnumFacing(string s)
        {
            return Get(s,Registry);
        }
        
        public static implicit operator string(EnumFacing facing)
        {
            return facing.Id;
        }
    }
}
