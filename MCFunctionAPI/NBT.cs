using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class NBT
    {

        private IDictionary<string, object> map;
        private string str;

        public NBT()
        {
            map = new Dictionary<string, object>();
        }

        public NBT(string str)
        {
            this.str = str;
        }

        public NBT(NBT nbt)
        {
            this.map = nbt.map;
            this.str = nbt.str;
        }

        public NBT Set(string key, object value)
        {
            switch (value)
            {
                case int i:
                case long lo:
                case string st:
                case bool bo:
                case short sh:
                case byte by:
                case float f:
                case double d:
                case object li when li.GetType() == typeof(IList<>):
                case NBT n:
                    break;
                default:
                    throw new ArgumentException($"Invalid value type {value.GetType().Name}");
                case null:
                    throw new ArgumentNullException(nameof(value));
            }
            map.Add(key, value);
            return this;
        }

        public virtual bool IsJson()
        {
            return false;
        }

        public object this[string key]
        {
            get
            {
                if (str != null)
                {
                    return str;
                }
                if (map.TryGetValue(key, out object o))
                {
                    return o;
                }
                return null;
            }
            set
            {
                if (str == null)
                {
                    Set(key, value);
                }
            }
        }

        public override string ToString()
        {
            return str ?? $"{{{string.Join(",", from entry in map select $"{(IsJson()?$"\"{entry.Key}\"":entry.Key)}:{entry.Value.ToNBTString(IsJson())}")}}}";
        }

        public static implicit operator NBT(string s)
        {
            return new NBT(s);
        }

        public bool IsEmpty()
        {
            return str == null ? map.Count == 0 : str.Length == 0;
        }
    }
}
