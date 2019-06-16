using System;
using System.Collections.Generic;

namespace MCFunctionAPI.LootTables
{
    public class LootFunction : ExtendsNBT<LootFunction>
    {
        public string Id { get; }
        public List<Condition> Conditions = new List<Condition>();

        public LootFunction(string id)
        {
            Id = id;
        }

        private static LootFunction New(string id)
        {
            return new LootFunction(id);
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

    public class CopyName : LootFunction
    {
        public string Id => "copy_name";
        

        public NBT ToNBT()
        {
            return new NBT().Set("source", "block_entity");
        }
    }

    public class CopyNBT : LootFunction
    {
        public string Id => "copy_nbt";

        public NBTSource? Source { get; set; }

        public List<NBTOps> Ops { get; private set; }

        public NBT ToNBT()
        {
            return new NBT().Set("source", Source == null ? null : FunctionWriter.LowerCase(Source.ToString())).Set("ops", Ops);
        }

        public enum NBTSource
        {
            BlockEntity,
            This,
            Killer,
            KillerPlayer
        }

        public class NBTOps
        {
            public string source;
            public string target;
            public Operation op;
        }

        public enum Operation
        {
            Replace,
            Append,
            Merge
        }
    }

    public class SetName : LootFunction
    {
        public string Id => throw new System.NotImplementedException();

        public string name;
        public Source? source;

        public NBT ToNBT()
        {
            return new NBT().Set("name", name).Set("entity", source == null ? null : FunctionWriter.LowerCase(source.ToString()));
        }
    }
}