using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    class ToStringBuilder
    {

        private string str;
        public Stringifier stringifier;

        public delegate string Stringifier(object obj);

        public ToStringBuilder(string initial)
        {
            this.str = initial;
        }

        public ToStringBuilder(string initial, Stringifier stringifier)
        {
            this.str = initial;
            this.stringifier = stringifier;
        }

        public ToStringBuilder Append(string s)
        {
            str += s;
            return this;
        }

        public ToStringBuilder Append(object o)
        {
            str += stringifier == null ? o.ToString() : stringifier(o);
            return this;
        }

        public ToStringBuilder AppendAll(string separator, params object[] objs) 
        {
            return Append(string.Join(separator, from o in objs select stringifier == null ? o.ToString() : stringifier(o)));
        }

        public override string ToString()
        {
            return str;
        }

        public static ToStringBuilder operator +(ToStringBuilder a, string b)
        {
            return a.Append(b);
        }

        public static implicit operator ToStringBuilder(string initial)
        {
            return new ToStringBuilder(initial);
        }

        public static implicit operator string(ToStringBuilder b)
        {
            return b.str;
        }

    }
}
