using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Utils
    {
        public static List<T> NullIfEmpty<T>(List<T> e)
        {
            return e.Count() == 0 ? default : e;
        }
    }
}
