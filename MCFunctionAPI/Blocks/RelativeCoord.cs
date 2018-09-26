using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class RelativeCoord
    {

        public double Value { get; set; }
        private bool relative;

        public RelativeCoord(double value) : this(value,false)
        {

        }

        public RelativeCoord(double value, bool relative)
        {
            Value = value;
            this.relative = relative;
        }

        public static RelativeCoord Absolute(double d)
        {
            if (MethodBase.GetCurrentMethod().DeclaringType == typeof(RotativeCoord))
            {
                return new RotativeCoord(d);
            }
            return new RelativeCoord(d);
        }

        public static RelativeCoord Relative(double d)
        {
            return new RelativeCoord(d,true);
        }

        public override string ToString()
        {
            return $"{(relative ? "~" : "")}{(Value == 0 && relative ? "" : Value.ToString())}";
        }

        public static implicit operator RelativeCoord(double d)
        {
            return new RelativeCoord(d);
        }

        public static implicit operator RelativeCoord(string s)
        {
            if (s[0] == '~')
            {
                return new RelativeCoord(s.Length == 1 ? 0 : double.Parse(s.Substring(1)));
            }
            return new RelativeCoord(double.Parse(s));
        }


    }
    
}
