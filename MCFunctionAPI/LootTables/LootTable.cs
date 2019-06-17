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

        public string Name;
        public TableType Type { get; set; }

        /// <summary>
        /// A list of all pools for this loot table. Each pool used will generate items from its list of items based on the number of rolls. Pools are applied in order.
        /// </summary>
        public List<Pool> Pools = new List<Pool>();

        public LootTable(string name, TableType type)
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
            return new NBT().Set("type", FunctionWriter.LowerCase(Type.ToString())).Set("pools", Pools).ToString(true, true);
        }

        public IEnumerator<Pool> GetEnumerator()
        {
            return Pools.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
