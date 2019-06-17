using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Utils
    {
        public static IList<T> NullIfEmpty<T>(IList<T> e)
        {
            return e.Count() == 0 ? default : e;
        }

        public static string LowerCase(string name)
        {
            string s = "";
            for (int i = 0; i < name.Length; i++)
            {

                char n = name[i];
                if (i != 0)
                {
                    if (char.IsUpper(n))
                    {
                        if (i < name.Length - 1)
                        {
                            return s + "_" + char.ToLower(n) + LowerCase(name.Substring(i + 1));
                        }
                        return s + char.ToLower(n);
                    }
                }
                s += char.ToLower(n);
            }
            return s;
        }

        public static string UpperCase(string name, bool space)
        {
            string s = "";
            bool upper = true;
            for (int i = 0; i < name.Length; i++)
            {
                if (upper)
                {
                    s += char.ToUpper(name[i]);
                    upper = false;
                }
                else if (name[i] == '_')
                {
                    if (space)
                    {
                        s += ' ';
                    }
                    upper = true;
                }
                else
                {
                    s += name[i];
                }
            }
            return s;
        }
    }
}
