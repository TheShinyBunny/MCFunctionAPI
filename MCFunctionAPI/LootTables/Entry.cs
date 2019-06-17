using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
    /// <summary>
    /// An entry in a loot <see cref="Pool"/>. Every pool can contain multiple entries, and only one is used based on weighted random selection.
    /// </summary>
    public class Entry : IFunctionable<Entry>, INBTSerializable
    {
        /// <summary>
        /// The type of entry. 
        /// </summary>
        public EntryType Type { get; set; }
        /// <summary>
        /// Determines conditions for this entry to be used. If multiple conditions are specified, all must pass.
        /// </summary>
        public List<Condition> Conditions = new List<Condition>();
        /// <summary>
        /// The name of the component to use in the pool, depends on the <see cref="Type"/>
        /// </summary>
        public ResourceLocation Name { get; set; }
        public Entry[] Children { get; set; }
        /// <summary>
        /// For <see cref="EntryType.Tag"/>, if set to true, it chooses one item of the tag, each with the same weight and quality. If false, it uses all the items in the tag.
        /// </summary>
        public bool Expand { get; set; }
        /// <summary>
        /// A list of functions to apply, in order, to the item of this entry.
        /// </summary>
        public List<LootFunction> Functions = new List<LootFunction>();
        /// <summary>
        /// The chance for this entry to be chosen out of the pool's entries. The chance is (<code>weight / sum of all entry weights in the pool</code>)
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Modifies the entry's weight based on the player's luck attribute. Formula is floor(weight + (quality * generic.luck)).
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// Creates an item entry
        /// </summary>
        /// <param name="item">The item ID to generate</param>
        public Entry(Item item)
        {
            this.Type = EntryType.Item;
            Name = item.Id;
        }

        public Entry(ItemTag tag)
        {
            this.Type = EntryType.Tag;
            Name = tag.Id;
        }

        public Entry(Namespace ns, LootTable lootTable)
        {
            this.Type = EntryType.LootTable;
            Name = new ResourceLocation(ns, Namespace.GetLootTableTypeDir(lootTable.Type) + "/" + lootTable.Name);
        }

        public override void AddFunction(LootFunction f)
        {
            Functions.Add(f);
        }

        public override void AddCondition(Condition c)
        {
            Conditions.Add(c);
        }

        public object ToNBT()
        {
            return new NBT().Set("type", FunctionWriter.LowerCase(Type.ToString())).Set("name",Name).Set("conditions", Utils.NullIfEmpty(Conditions)).Set("expand", Type == EntryType.Tag ? Expand : (bool?)null).Set("functions", Utils.NullIfEmpty(Functions)).Set("weight", Weight == 0 ? (int?)null : Weight).Set("quality", Quality == 0 ? (int?)null : Quality);
        }
    }

    public enum EntryType
    {
        Dynamic,
        Tag,
        Item,
        Empty,
        Alternatives,
        Sequence,
        Group,
        LootTable
    }
}
