using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
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

        /// <summary>
        /// Checks for the used tool to kill/mine/fish/etc.
        /// </summary>
        /// <param name="item">Predicate for the item used</param>
        /// <returns></returns>
        public This UsedTool(ItemPredicate item)
        {
            AddCondition(Condition.MatchTool(item));
            return this as This;
        }

        /// <summary>
        /// Joins conditions from parameter terms with "or"
        /// </summary>
        /// <param name="terms">A list of conditions to join using 'or'</param>
        /// <returns></returns>
        public This Alternative(params Condition[] terms)
        {
            AddCondition(Condition.Alternative(terms));
            return this as This;
        }

        /// <summary>
        ///  Checks whether the broken block had a specific block state.
        /// </summary>
        /// <param name="block">The block id + block state to check</param>
        /// <returns></returns>
        public This BlockStateIs(Block block)
        {
            AddCondition(Condition.BlockState(block));
            return this as This;
        }

        public This HasEntity()
        {
            AddCondition(Condition.EntityPresent);
            return this as This;
        }

        public This TestScore(Source entity, ScoreSet scores)
        {
            AddCondition(Condition.EntityScores(entity, scores));
            return this as This;
        }

        public abstract void AddCondition(Condition c);

    }
}
