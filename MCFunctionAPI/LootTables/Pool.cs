using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    /// <summary>
    /// A pool is a list of <see cref="Entry"/> objects.
    /// When generating items for the loot, every pool is activated.
    /// </summary>
    public class Pool : IConditionable<Pool>, IEnumerable<Entry>, INBTSerializable
    {

        /// <summary>
        /// Determines conditions for this pool to be used. If multiple conditions are specified, all must pass.
        /// </summary>
        public List<Condition> Conditions = new List<Condition>();

        /// <summary>
        /// Specifies the number of times to activate this pool
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

        /// <summary>
        /// Creates a new pool.
        /// </summary>
        /// <param name="rolls"></param>
        public Pool(IntRange rolls) : this(rolls,null)
        {

        }

        public Pool(IntRange rolls, DoubleRange bonusRolls)
        {
            Rolls = rolls;
            BonusRolls = bonusRolls;
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
