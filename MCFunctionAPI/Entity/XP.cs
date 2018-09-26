using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class XP
    {

        public Entities owner;
        private Value p;
        private Value l;
        public Value Points
        {
            get
            {
                return p ?? (p = new Value(this,"points"));
            }
            set
            {
                FunctionWriter.Write($"xp set {owner} {value} points");
                p.holder = this;
                p.pl = "points";
                p = value;
            }
        }
        public Value Levels
        {
            get
            {
                return l ?? (l = new Value(this, "levels"));
            }
            set
            {
                FunctionWriter.Write($"xp set {owner} {value} levels");
                l.holder = this;
                l.pl = "levels";
                l = value;
            }
        }

        public XP(Entities owner)
        {
            this.owner = owner;
        }
    }

    public class Value
    {

        private int val;
        public XP holder;
        public string pl;

        private Value(int v)
        {
            this.val = v;
        }

        public Value(XP holder, string pl)
        {
            this.holder = holder;
            this.pl = pl;
        }

        public static Value Of(int i)
        {
            return new Value(i);
        }

        public void Query()
        {
            FunctionWriter.Write($"xp query {holder.owner} {pl}");
        }

        public static implicit operator Value(int i)
        {
            return Value.Of(i);
        }

        public static Value operator +(Value v, int i)
        {
            FunctionWriter.Write($"xp add {v.holder.owner} {i} {v.pl}");
            return v;
        }
        public static Value operator -(Value v, int i)
        {
            FunctionWriter.Write($"xp add {v.holder.owner} -{i} {v.pl}");
            return v;
        }

        public static Value operator ++(Value v)
        {
            FunctionWriter.Write($"xp add {v.holder.owner} {1} {v.pl}");
            return v;
        }
        public static Value operator --(Value v)
        {
            FunctionWriter.Write($"xp add {v.holder.owner} -1 {v.pl}");
            return v;
        }

        public override string ToString()
        {
            return val.ToString();
        }



    }
}
