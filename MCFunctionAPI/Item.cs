using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Item : ITaggable
    {

        private ResourceLocation id;
        private NBT nbt;

        public ResourceLocation Id => id;

        public Item(ResourceLocation id) : this(id, new NBT())
        { }
        
        public Item(ResourceLocation id, NBT nbt)
        {
            this.id = id;
            this.nbt = nbt;
        }
        

        public static implicit operator Item(string s)
        {
            int curly = s.IndexOf('{');
            ResourceLocation id = s.Substring(0, curly == -1 ? s.Length : curly);
            NBT nbt = new NBT();
            if (curly != -1)
            {
                nbt = s.Substring(curly);
            }
            return new Item(id, nbt);
        }

        public override string ToString()
        {
            return $"{id}{(nbt.IsEmpty() ? "" : nbt)}";
        }

        public NBT ToNBT()
        {
            NBT nbt = new NBT();
            nbt.Set("id", id);
            nbt.Set("tag", this.nbt);
            return nbt;
        }
    }
}
