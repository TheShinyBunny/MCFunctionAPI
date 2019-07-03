using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    public class LootTable : IEnumerable<Pool>
    {

        public string Name { get; }
        public TableType Type { get; set; }

        /// <summary>
        /// A list of all pools for this loot table. Each pool used will generate items from its list of items based on the number of rolls. Pools are applied in order.
        /// </summary>
        public List<Pool> Pools = new List<Pool>();

        /// <summary>
        /// Creates a new empty loot table
        /// </summary>
        /// <param name="name">The name of the loot table. Will be used as the file's name</param>
        /// <param name="type">The type of loot table. Used to determine the loot_tables sub directory to place the loot table file</param>
        public LootTable(string name, TableType type = TableType.Generic)
        {
            this.Name = name;
            this.Type = type;
        }

        public LootTable Add(Pool pool)
        {
            Pools.Add(pool);
            return this;
        }

        public string ToJson()
        {
            return new NBT().Set("type", Utils.LowerCase(Type.ToString())).Set("pools", Pools).ToString(true, true);
        }

        public IEnumerator<Pool> GetEnumerator()
        {
            return Pools.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator LootTable(string id)
        {
            return new LootTable(id);
        }

    }

    public enum TableType
    {
        Empty,
        Entity,
        Block,
        Chest,
        Fishing,
        AdvancementReward,
        Gift,
        Generic
    }
}
