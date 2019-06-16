using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Blocks
{
    public class RotativeCoord : RelativeCoord
    {

        public bool rotated;

        public RotativeCoord(double value) : base(value)
        {

        }

        public RotativeCoord(bool relative, double value) : base(value,relative)
        {

        }


        public RotativeCoord(double value, bool rotated) : base(value)
        {
            this.rotated = rotated;
        }

        public static RotativeCoord Rotated(double d)
        {
            return new RotativeCoord(d, true);
        }
        

        public static implicit operator RotativeCoord(double d)
        {
            return new RotativeCoord(d);
        }

        public static implicit operator RotativeCoord(string s)
        {
            if (s[0] == '~')
            {
                return new RotativeCoord(true, s.Length == 1 ? 0 : double.Parse(s.Substring(1)));
            } else if (s[0] == '^')
            {
                return new RotativeCoord(s.Length == 1 ? 0 : double.Parse(s.Substring(1)),true);
            }
            return new RotativeCoord(double.Parse(s));
        }

        public RotativeCoord Rotated()
        {
            return new RotativeCoord(Value, true);
        }

        public override string ToString()
        {
            return rotated ? $"^{(Value == 0 ? "" : Value.ToString())}" : base.ToString();
        }

        public static RotativeCoord operator +(RotativeCoord coord, double d)
        {
            return new RotativeCoord(coord.Value + d)
            {
                rotated = coord.rotated,
                relative = coord.relative
            };
        }

        public static RotativeCoord operator -(RotativeCoord coord, double d)
        {
            return new RotativeCoord(coord.Value - d)
            {
                rotated = coord.rotated,
                relative = coord.relative
            };
        }
    }
}
