using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class TagList
    {

        private Entities owner;

        public TagList(Entities owner)
        {
            this.owner = owner;
        }

        public void Add(string tag)
        {
            FunctionWriter.Write($"tag {owner} add {tag}");
        }

        public void Remove(string tag)
        {
            FunctionWriter.Write($"tag {owner} remove {tag}");
        }

        public void List()
        {
            FunctionWriter.Write($"tag {owner} list");
        }

        public static TagList operator +(TagList list, string tag)
        {
            list.Add(tag);
            return list;
        }

        public static TagList operator -(TagList list, string tag)
        {
            list.Remove(tag);
            return list;
        }

    }
}
