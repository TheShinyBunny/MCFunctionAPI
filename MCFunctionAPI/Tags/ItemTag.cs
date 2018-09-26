using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Tags
{
    public class ItemTag : Tag<Item>
    {
        public ItemTag(ResourceLocation id) : base(id)
        {
        }

        public override string FolderName => "items";


    }
}
