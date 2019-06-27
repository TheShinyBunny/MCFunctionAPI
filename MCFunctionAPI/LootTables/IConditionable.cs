using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public abstract class IConditionable<This> where This : IConditionable<This>
    {

        /// <summary>
        /// Used only if the killer is a player
        /// </summary>
        /// <returns></returns>
        public This KilledByPlayer()
        {
            AddCondition(Condition.KilledByPlayer);
            return this as This;
        }

        /// <summary>
        /// Used only if a random chance is passed
        /// </summary>
        /// <param name="chance">The chance. 1.0 = 100% of success</param>
        /// <returns></returns>
        public This RandomChance(float chance)
        {
            AddCondition(Condition.RandomChance(chance,null));
            return this as This;
        }

        /// <summary>
        /// Used only if a random chance, that is modified by the looting enchantment, is passed.
        /// </summary>
        /// <param name="chance">The chance. 1.0 = 100% of success</param>
        /// <param name="lootingMultiplier">The chance to add per looting enchantment level (chance = chance + (lootingMultiplier * killerLootingLevel</param>
        /// <returns></returns>
        public This RandomChance(float chance, float lootingMultiplier)
        {
            AddCondition(Condition.RandomChance(chance,lootingMultiplier));
            return this as This;
        }

        public This UsedTool(ItemPredicate item)
        {
            AddCondition(Condition.MatchTool(item));
            return this as This;
        }

        public abstract void AddCondition(Condition c);

    }
}
