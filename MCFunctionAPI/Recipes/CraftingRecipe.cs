using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public abstract class CraftingRecipe : Recipe
    {
        
        private int resultCount;

        public CraftingRecipe(ResourceLocation id) : base(id)
        {
        }

        public CraftingRecipe(ResourceLocation id, string group) : base(id, group)
        {
        }

        public void SetResult(ResourceLocation item)
        {
            SetResult(item, 1);
        }

        public void SetResult(ResourceLocation item, int count)
        {
            Result = item;
            resultCount = count;
        }
    }
}
