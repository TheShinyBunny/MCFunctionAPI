using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public class SmeltingRecipe : Recipe
    {

        private ResourceLocation ingredient;
        private double experience;
        private int cookingTime;

        public SmeltingRecipe(ResourceLocation id) : base(id)
        {
        }

        public SmeltingRecipe(ResourceLocation id, string group) : base(id, group)
        {
        }

        public void SetResult(ResourceLocation item)
        {
            this.Result = item;
        }

        public void SetIngredient(ResourceLocation item)
        {
            ingredient = item;
        }

        public void SetExperience(double xp)
        {
            experience = xp;
        }

        public void SetCookingTime(int ticks)
        {
            cookingTime = ticks;
        }
    }
}
