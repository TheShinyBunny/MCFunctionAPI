using MCFunctionAPI.Advancements;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public class Condition : ExtendsNBT<Condition>
    {

        private static Condition New(string id)
        {
            return new Condition().Set("condition", id);
        }

        public static Condition Alternative(params Condition[] terms)
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

        public static Condition MatchTool(ItemCondition item)
        {
            return New("match_tool").Set("predicate", item);
        }

        public static Condition Inverted(Condition cond)
        {
            return New("inverted").Set("term", cond);
        }

        public static Condition LocationCheck(LocationCondition loc)
        {
            return New("location_check").Set("predicate", loc);
        }

        public static Condition SurvivesExplosion = New("survives_explosion");

        public static Condition EntityScores(Source entity, ScoreSet scores)
        {
            return New("entity_scores").Set("entity", Utils.LowerCase(entity.ToString())).Set("scores", scores);
        }
    }
}
