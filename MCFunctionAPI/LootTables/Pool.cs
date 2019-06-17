using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public class Pool : IConditionable<Pool>, IEnumerable<Entry>, INBTSerializable
    {

        /// <summary>
        /// Determines conditions for this pool to be used. If multiple conditions are specified, all must pass.
        /// </summary>
        public List<Condition> Conditions = new List<Condition>();

        /// <summary>
        /// Specifies the number of rolls on the pool
        /// </summary>
        public IntRange Rolls { get; set; }
        /// <summary>
        ///  Specifies the number of bonus rolls on the pool per point of luck. Rounded down after multiplying.
        /// </summary>
        public DoubleRange BonusRolls { get; set; }
        /// <summary>
        /// A list of all things that can be produced by this pool. One entry is chosen per roll as a weighted random selection from all entries without failing conditions.
        /// </summary>
        public List<Entry> Entries = new List<Entry>();

        public Pool(IntRange rolls)
        {
            Rolls = rolls;
        }

        public Pool()
        {
            Rolls = 1;
        }

        public void Add(Entry item)
        {
            Entries.Add(item);
        }

        public Pool AddEntry(Entry entry)
        {
            Entries.Add(entry);
            return this;
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            return Entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void AddCondition(Condition c)
        {
            Conditions.Add(c);
        }

        public object ToNBT()
        {
            return new NBT().Set("rolls", Rolls).Set("bonus_rolls", BonusRolls).Set("conditions", Utils.NullIfEmpty(Conditions)).Set("entries", Entries);
        }
    }
}
