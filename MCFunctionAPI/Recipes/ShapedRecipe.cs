using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Recipes
{
    public class ShapedRecipe : CraftingRecipe
    {

        private Pattern pattern;

        public ShapedRecipe(ResourceLocation id) : base(id)
        {
        }

        public ShapedRecipe(ResourceLocation id, string group) : base(id, group)
        {
        }

        public void SetPattern(Pattern p)
        {
            this.pattern = p;
        }
    }

    public class Pattern
    {

        private Dictionary<char,ResourceLocation> keys;
        private string[] rows = new string[3];

        public string this[int row]
        {
            set
            {
                rows[row] = value;
            }
        }
        
        public void SetItems(params ResourceLocation[] items)
        {

        }

    }
}
