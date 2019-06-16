using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class NBT : INBTSerializable
    {

        private IDictionary<string, object> map;
        private string str;
        private bool json;

        public NBT()
        {
            map = new Dictionary<string, object>();
        }

        public NBT(string str)
        {
            this.str = str;
        }

        public NBT(bool json): this()
        {
            this.json = json;
        }

        public NBT(NBT nbt)
        {
            this.map = nbt.map;
            this.str = nbt.str;
        }

        public virtual NBT Set(string key, int? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, double? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, float? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, byte? value)
        {
            return SetAny(key, value);
        }

        public void SetJson(bool json)
        {
            this.json = json;
        }

        public virtual NBT Set(string key, string value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, long? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, short? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, bool? value)
        {
            return SetAny(key, value);
        }

        public virtual NBT Set(string key, INBTSerializable value)
        {
            return SetAny(key, value?.ToNBT());
        }

        public virtual NBT Set<T>(string key, IList<T> list) where T : INBTSerializable
        {
            return SetAny(key, list);
        }

        public virtual NBT SetAny(string key, object value)
        {
            if (value != null && !(value is NBT && (value as NBT).IsEmpty()))
            {
                if (key.Contains("."))
                {
                    string sub = key.Substring(0, key.IndexOf("."));
                    if (map.TryGetValue(sub, out object val))
                    {
                        if (val is NBT)
                        {
                            SetAny(key.Substring(key.IndexOf(".") + 1), value);
                        }
                    } else
                    {
                        map.Add(sub, new NBT().SetAny(key.Substring(key.IndexOf(".") + 1), value));
                    }
                } else
                {
                    map.Add(key, value);
                }
            }
            return this;
        }

        public bool IsJson()
        {
            return json;
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
                    SetAny(key, value);
                }
            }
        }

        public override string ToString()
        {
            return str ?? $"{{{string.Join(",", from entry in map select $"{(json?$"\"{entry.Key}\"":entry.Key)}:{entry.Value.ToNBTString(IsJson())}")}}}";
        }

        public static implicit operator NBT(string s)
        {
            return new NBT(s);
        }

        public bool IsEmpty()
        {
            return str == null ? map.Count == 0 : str.Length == 0;
        }

        public object ToNBT()
        {
            return this;
        }
    }

    public interface INBTSerializable
    {
        object ToNBT();
    }
}
