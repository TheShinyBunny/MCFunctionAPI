using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    static class Extensions
    {

        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            foreach (T o in values)
            {
                if (obj.Equals(o))
                {
                    return true;
                }
            }
            return false;
        }

        public static string ToNBTString(this object obj, bool json)
        {
            switch (obj)
            {
                case string v:
                    return "\"" + v + "\"";
                case IEnumerable ie:
                    return $"[{string.Join(",", from item in ie.Cast<object>() select ie.ToNBTString(json))}]";
                case int i:
                case NBT n:
                    return obj.ToString();
                case bool b:
                    return json ? b.ToString().ToLower() : b ? "1b" : "0b";
                default:
                    return obj.ToString() + obj.GetType().Name[0];
            }
        }

        public static bool EqualsIgnoreCase(this string str, string to)
        {
            return str.ToLower().Equals(to.ToLower());
        }
    }
}
