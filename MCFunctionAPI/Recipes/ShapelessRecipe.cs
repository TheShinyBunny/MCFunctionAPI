using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public class ShapelessRecipe : CraftingRecipe
    {

        private List<ResourceLocation> ingredients;

        public ShapelessRecipe(ResourceLocation id) : base(id)
        {
        }

        public ShapelessRecipe(ResourceLocation id, string group) : base(id, group)
        {
        }

        public void SetIngredients(params ResourceLocation[] items)
        {
            ingredients = items.ToList();
        }

    }
}
