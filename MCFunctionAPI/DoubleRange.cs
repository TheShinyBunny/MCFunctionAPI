using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class DoubleRange : SingleArgument, INBTSerializable
    {
        private double? min;
        private double? max;
        private double? exact;

        public DoubleRange(double? min, double? max)
        {
            this.min = min;
            this.max = max;
            this.exact = null;
        }

        public DoubleRange(double exact) : this(null, null)
        {
            this.exact = exact;
        }

        public static implicit operator DoubleRange(string s)
        {
            if (s.Contains(".."))
            {
                int index = s.IndexOf("..");
                string smin = s.Substring(0, index);
                double? min = null;
                if (smin != "")
                {
                    min = double.Parse(smin);
                }
                double? max = null;
                if (index + 2 < s.Length)
                {
                    string smax = s.Substring(index + 2);
                    if (smax != "")
                    {
                        max = double.Parse(smax);
                    }
                }
                return new DoubleRange(min, max);
            }
            return new DoubleRange(double.Parse(s));
        }

        public static implicit operator DoubleRange(double d)
        {
            return new DoubleRange(d);
        }

        public override string ToString()
        {
            string s = "";
            if (min != null)
                s += min;
            if (exact == null)
                s += "..";
            else
                s += exact;
            if (max != null)
                s += max;
            return s;
        }

        public static DoubleRange Of(double min, int max)
        {
            return new DoubleRange(min, max);
        }

        public override string BuildValue()
        {
            return ToString();
        }

        public object ToNBT()
        {
            return exact == null ? (object)new NBT().Set("min", min).Set("max", max) : exact;
        }
    }
}
