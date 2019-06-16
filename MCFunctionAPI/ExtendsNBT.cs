using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class ExtendsNBT<This> : INBTSerializable where This : ExtendsNBT<This>
    {

        private NBT del = new NBT();

        public This Set(string key, int? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, double? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, float? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, byte? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, string value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, long? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, short? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, bool? value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set(string key, INBTSerializable value)
        {
            del.Set(key, value);
            return this as This;
        }

        public This Set<T>(string key, IList<T> list)
        {
            del.Set(key, list);
            return this as This;
        }

        public This SetAny(string key, object value)
        {
            del.SetAny(key, value);
            return this as This;
        }

        public void SetJson(bool json)
        {
            del.SetJson(json);
        }

        public bool IsJson()
        {
            return del.IsJson();
        }

        public object this[string key]
        {
            get => del[key];
            set => del[key] = value;
        }

        public override string ToString()
        {
            return del.ToString();
        }

        public bool IsEmpty()
        {
            return del.IsEmpty();
        }

        public object ToNBT()
        {
            return del;
        }
    }
}
