using System;
using System.Collections.Generic;

namespace MCFunctionAPI.LootTables
{
    public class LootFunction : ExtendsNBT<LootFunction>
    {

        public List<Condition> Conditions = new List<Condition>();

        private static LootFunction New(string id)
        {
            return new LootFunction().Set("function", id);
        }

        public static LootFunction CopyName = New("copy_name").Set("source", "block_entity");

        public static LootFunction LootingEnchant(IntRange count)
        {
            return New("looting_enchant").Set("count", count);
        }

        public static LootFunction SetCount(IntRange count)
        {
            return New("set_count").Set("count", count);
        }

    }

    public enum Source
    {
        This,
        Killer,
        KillerPlayer
    }

}