using MCFunctionAPI.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Tags
{
    public class BlockTag : Tag<Block>
    {
        public override string FolderName => "blocks";


        public BlockTag(ResourceLocation id) : base(id)
        {

        }


    }
}
