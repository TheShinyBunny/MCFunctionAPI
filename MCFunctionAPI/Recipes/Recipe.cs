
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public abstract class Recipe
    {

        public ResourceLocation Id { get; private set; }
        public string Group { get; set; }
        public ResourceLocation Result { get; set; }

        public Recipe(ResourceLocation id)
        {
            this.Id = id;
        }

        public Recipe(ResourceLocation id, string group) : this(id)
        {
            this.Group = group;
        }
        
    }
}
