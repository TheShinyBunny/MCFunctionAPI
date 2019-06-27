using MCFunctionAPI.Advancements;
using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Item : ITaggable, INBTSerializable
    {
        public readonly NBT nbt;

        public ResourceLocation Id { get; }

        public Item(ResourceLocation id) : this(id, new NBT())
        { }
        
        public Item(ResourceLocation id, NBT nbt)
        {
            this.Id = id;
            this.nbt = nbt;
        }

        public static Item Of(ResourceLocation id)
        {
            return new Item(id);
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
            return $"{Id}{(nbt.IsEmpty() ? "" : nbt)}";
        }

        public object ToNBT()
        {
            NBT nbt = new NBT();
            nbt.Set("id", Id.ToString());
            nbt.Set("Count", (short)1);
            nbt.Set("tag", this.nbt);
            return nbt;
        }

        public Item SetDisplayName(string displayName)
        {
            return SetProperty("display.Name", "{\\\"text\\\":\\\"" + displayName + "\\\",\\\"italic\\\":false}");
        }

        public Item SetProperty(string path, object value)
        {
            nbt.SetAny(path, value);
            return this;
        }

        public Item SetDamage(int damage)
        {
            return SetProperty("Damage",(short)damage);
        }

        public Item SetBlockEntityTag(NBT tag)
        {
            return SetProperty("BlockEntityTag", tag);
        }

        public Item SetBlockContainerItems(IList<Item> items)
        {
            return SetProperty("BlockEntityTag.Items", items);
        }
    }
}
