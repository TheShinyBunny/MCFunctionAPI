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

        public static string SubstringIndexed(this string str, int startIndex, int endIndex)
        {
            return str.Substring(startIndex, endIndex - startIndex);
        }

        public static string ToNBTString(this object obj, bool json)
        {
            switch (obj)
            {
                case string v:
                default:
                    return "\"" + obj + "\"";
                case IEnumerable ie:
                    return $"[{string.Join(",", from item in ie.Cast<object>() select item.ToNBTString(json))}]";
                case int i:
                    return obj.ToString();
                case double d:
                    return obj.ToString() + 'd';
                case float f:
                    return obj.ToString() + 'f';
                case byte b:
                    return obj.ToString() + 'b';
                case short s:
                    return obj.ToString() + 's';
                case long l:
                    return obj.ToString() + 'L';
                case NBT n:
                    n.SetJson(json);
                    return n.ToString();
                case INBTSerializable ser:
                    return ser.ToNBT().ToNBTString(json);
                case bool b:
                    return json ? b.ToString().ToLower() : b ? "1b" : "0b";
                    
            }
        }

        public static bool EqualsIgnoreCase(this string str, string to)
        {
            return str.ToLower().Equals(to.ToLower());
        }
    }
}
