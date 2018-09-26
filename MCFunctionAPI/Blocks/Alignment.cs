using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class Alignment
    {

        private bool x;
        private bool y;
        private bool z;

        public Alignment(bool x, bool y, bool z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public static Alignment Of(bool x, bool y, bool z)
        {
            return new Alignment(x, y, z);
        }

        public static implicit operator Alignment(string s)
        {
            char[] c = s.ToCharArray();
            return new Alignment(c.Contains('x'), c.Contains('y'), c.Contains('z'));
        }

        public override string ToString()
        {
            return (x ? "x" : "") + (y ? "y" : "") + (z ? "z" : "");
        }
    }
}
