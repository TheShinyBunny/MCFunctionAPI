using MCFunctionAPI.LootTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{
    public class Reward : INBTSerializable
    {

        private string name;
        private object value;

        private Reward(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public static Reward Function(Namespace ns, CommandWrapper.Function f)
        {
            return new Reward("function", FunctionWriter.GetFunctionPath(ns, f));
        }

        public static Reward Function(Namespace ns, CommandWrapper.ParameterFunction pf, string param)
        {
            return new Reward("function", FunctionWriter.GetFunctionPath(ns, pf, param));
        }

        public static Reward Loot(params ResourceLocation[] loot)
        {
            return new Reward("loot",loot);
        }

        public static Reward XP(int xp)
        {
            return new Reward("experience", xp);
        }

        public static Reward Recipes(params ResourceLocation[] recipes)
        {
            return new Reward("recipes", recipes);
        }

        public object ToNBT()
        {
            return new NBT().SetAny(name, value);
        }
    }
}
