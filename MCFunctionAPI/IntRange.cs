using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class IntRange : SingleArgument, INBTSerializable
    {
        private int? min;
        private int? max;
        private int? exact;

        public IntRange(int? min, int? max)
        {
            this.min = min;
            this.max = max;
            this.exact = null;
        }

        public IntRange(int exact) : this(null, null)
        {
            this.exact = exact;
        }

        public static implicit operator IntRange(string s)
        {
            if (s.Contains(".."))
            {
                int index = s.IndexOf("..");
                string smin = s.Substring(0, index);
                int? min = null;
                if (smin != "")
                {
                    min = int.Parse(smin);
                }
                int? max = null;
                if (index + 2 < s.Length)
                {
                    string smax = s.Substring(index + 2);
                    if (smax != "")
                    {
                        max = int.Parse(smax);
                    }
                }
                return new IntRange(min, max);
            }
            return new IntRange(int.Parse(s));
        }

        public static implicit operator IntRange(int i)
        {
            return new IntRange(i);
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

        public override string BuildValue()
        {
            return ToString();
        }

        public object ToNBT()
        {
            return exact == null ? (object)new NBT().Set("min", min).Set("max", max) : exact;
        }

        public static IntRange Between(int min, int max)
        {
            return new IntRange(min, max);
        }
    }
}
