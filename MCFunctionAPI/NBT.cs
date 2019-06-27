using System;
using System.Collections;
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
            this.map = new Dictionary<string,object>(nbt.map);
            this.str = nbt.str;
        }

        public NBT Copy()
        {
            return new NBT(this);
        }

        public NBT Set(string key, int? value)
        {
            return SetAny(key, value);
        }
        public virtual NBT Set(string key, double? value)
        {
            return SetAny(key, value);
        }
        public NBT Set(string key, float? value)
        {
            return SetAny(key, value);
        }
        public NBT Set(string key, byte? value)
        {
            return SetAny(key, value);
        }

        public NBT Set(string key, string value)
        {
            return SetAny(key, value);
        }
        public NBT Set(string key, long? value)
        {
            return SetAny(key, value);
        }
        public NBT Set(string key, short? value)
        {
            return SetAny(key, value);
        }
        public NBT Set(string key, bool? value)
        {
            return SetAny(key, value);
        }

        public NBT Set(string key, INBTSerializable value)
        {
            return SetAny(key, value?.ToNBT());
        }

        public NBT Set<T>(string key, IList<T> list) where T : INBTSerializable
        {
            return SetAny(key, list);
        }

        public NBT Set(string key, IList<string> stringList)
        {
            return SetAny(key, stringList);
        }

        public NBT Set(string key, IList<int> stringList)
        {
            return SetAny(key, stringList);
        }

        public NBT Set(string key, IList<double> stringList)
        {
            return SetAny(key, stringList);
        }

        public NBT Set(string key, IList<float> stringList)
        {
            return SetAny(key, stringList);
        }

        public NBT Set(string key, IList<byte> stringList)
        {
            return SetAny(key, stringList);
        }

        public NBT SetAny(string key, object value)
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

        public T Get<T>(string key)
        {
            if (str != null)
            {
                return default;
            }
            if (map.TryGetValue(key, out object o))
            {
                return (T)o;
            }
            return default;
        }


        public object this[string key]
        {
            get
            {
                return Get<object>(key);
            }
            set
            {
                if (str == null)
                {
                    SetAny(key, value);
                }
            }
        }

        public NBT MergeWith(NBT nbt)
        {
            foreach (var e in nbt.map)
            {
                map.Add(e);
            }
            return this;
        }

        public override string ToString()
        {
            return ToString(false,false);
        }

        public string ToString(bool json, bool prettyPrint)
        {
            if (str != null) return str;
            if (!prettyPrint) return $"{{{string.Join(",", from entry in map select $"{(json ? $"\"{entry.Key}\"" : entry.Key)}:{entry.Value.ToNBTString(json,null)}")}}}";
            return PrettyPrint(json,"");
        }

        public string ToString(bool json, string prettyIndent)
        {
            if (prettyIndent == null) return ToString(json, false);
            return PrettyPrint(json, prettyIndent);
        }

        private string PrettyPrint(bool json, string indent) {
            string s = "{\n";
            bool first = true;
            foreach (var e in map)
            {
                if (!first)
                {
                    s += ",\n";
                }
                first = false;
                s += indent + "    " + (json ? "\"" + e.Key + "\"" : e.Key) + ": ";

                if (e.Value is ICollection)
                {
                    s += "[\n";
                    bool first2 = true;
                    foreach (object o in e.Value as IEnumerable)
                    {
                        if (!first2)
                        {
                            s += ",\n";
                        }
                        s += indent + "        " + o.ToNBTString(json,indent + "        ");
                        first2 = false;
                    }
                    s += "\n" + indent + "    ]";
                } else
                {
                    s += e.Value.ToNBTString(json, indent + "    ");
                }
            }
            s += "\n" + indent + "}";
            return s;
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
