using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.LootTables
{
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
        public ResourceLocation Name { get; set; }
        public Entry[] Children { get; set; }
        public bool Expand { get; set; }
        public List<LootFunction> Functions = new List<LootFunction>();
        public int Weight { get; set; }
        public int Quality { get; set; }

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
