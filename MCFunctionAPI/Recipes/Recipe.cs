
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public abstract class Recipe
    {

        private ResourceLocation id;
        private string group;
        protected ResourceLocation result;

        public Recipe(ResourceLocation id)
        {
            this.id = id;
        }

        public Recipe(ResourceLocation id, string group) : this(id)
        {
            this.group = group;
        }

        public void SetGroup(string group)
        {
            this.group = group;
        }
        
    }
}
