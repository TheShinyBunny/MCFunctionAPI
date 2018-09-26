using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Tags
{
    public class FunctionTag : Tag<ResourceLocation>
    {
        public FunctionTag(ResourceLocation id) : base(id)
        {
        }

        public override string FolderName => "functions";


    }
}
