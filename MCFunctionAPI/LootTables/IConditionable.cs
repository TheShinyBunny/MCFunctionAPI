using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public abstract class IConditionable<This> where This : IConditionable<This>
    {

        public This KilledByPlayer()
        {
            AddCondition(Condition.KilledByPlayer);
            return this as This;
        }

        public This RandomChance(float chance)
        {
            AddCondition(Condition.RandomChance(chance,null));
            return this as This;
        }

        public This RandomChance(float chance, float lootingMultiplier)
        {
            AddCondition(Condition.RandomChance(chance,lootingMultiplier));
            return this as This;
        }

        public abstract void AddCondition(Condition c);

    }
}
