using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Anchor : EnumBase
    {

        public static Anchor Feet = new Anchor("feet");
        public static Anchor Eyes = new Anchor("eyes");

        public Anchor(string id) : base(id)
        {

        }
    }
}
