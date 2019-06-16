using MCFunctionAPI.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public class Condition : ExtendsNBT<Condition>
    {

        public string Id { get; }

        public Condition(string id)
        {
            this.Id = id;
        }

        private static Condition New(string id)
        {
            return new Condition(id);
        }

        public static Condition Alternative(List<Condition> terms)
        {
            return New("alternative").Set("terms", terms);
        }

        public static Condition BlockState(Block block)
        {
            return New("block_state_property").Set("block", block.Id).Set("properties",block.state);
        }

        public static Condition RandomChance(float chance, float? lootingMultiplier)
        {
            return New(lootingMultiplier == null ? "random_chance" : "random_chance_with_looting").Set("chance", chance).Set("looting_multiplier", lootingMultiplier);
        }

        public static Condition EntityPresent = New("entity_present");

        public static Condition KilledByPlayer = New("killed_by_player");

    }
}
