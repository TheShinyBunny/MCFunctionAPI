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

        public static LootFunction CopyNBT(NBTSource source, params NBTOp[] ops)
        {
            return New("copy_nbt").Set("source",Utils.LowerCase(source.ToString())).Set("ops", ops);
        }

        public static LootFunction EnchantRandomly(params Enchantment[] enchants)
        {
            return New("enchant_randomly").Set("enchantments", Utils.NullIfEmpty(enchants));
        }

        public static LootFunction EnchantWithLevels(IntRange levels, bool treasure = false)
        {
            return New("enchant_with_levels").Set("levels", levels).Set("treasure", treasure);
        }

        public static LootFunction ExplorationMap(string structure, string iconId, int zoom = 2, int searchRadius = 50, bool skipExistingChunks = true)
        {
            return New("exploration_map").Set("destination", structure).Set("decoration", iconId).Set("zoom", zoom).Set("search_radius", searchRadius).Set("skip_existing_chunks", skipExistingChunks);
        }

        public static LootFunction ExplosionDecay = New("explosion_decay");
        public static LootFunction FurnaceSmelt = New("furnace_smelt");
        public static LootFunction FillPlayerHead(Source source)
        {
            return New("fill_player_head").Set("entity", Utils.LowerCase(source.ToString()));
        }

        public static LootFunction LimitCount(IntRange limit)
        {
            return New("limit_count").Set("limit", limit);
        }

        public static LootFunction LootingEnchant(IntRange count)
        {
            return New("looting_enchant").Set("count", count);
        }

        public static LootFunction SetContents(params Item[] entries)
        {
            return New("set_contents").Set("entries", entries);
        }

        public static LootFunction SetCount(IntRange count)
        {
            return New("set_count").Set("count", count);
        }

        public static LootFunction SetDamage(IntRange damage)
        {
            return New("set_damage").Set("damage", damage);
        }

        public static LootFunction SetLore(TextComponent[] lore, Source @s, bool replace)
        {
            return New("set_lore").Set("lore", lore).Set("entity", Utils.LowerCase(@s.ToString())).Set("replace", replace);
        }

        public static LootFunction SetName(string name)
        {
            return New("set_name").Set("name", name);
        }

        public static LootFunction SetName(TextComponent name, Source @s)
        {
            return New("set_name").Set("name",name).Set("entity", Utils.LowerCase(@s.ToString()));
        }

        public static LootFunction SetNBT(NBT nbt)
        {
            return New("set_nbt").Set("tag", nbt.ToString());
        }

    }

    public class NBTOp : INBTSerializable
    {
        private string source;
        private string target;
        private Operation op;

        public NBTOp(string source, string target, Operation op)
        {
            this.source = source;
            this.target = target;
            this.op = op;
        }

        public object ToNBT()
        {
            return new NBT().Set("source", source).Set("target", target).Set("op", op.ToString().ToLower());
        }
    }

    public enum Operation
    {
        Replace,
        Append,
        Merge
    }

    public enum NBTSource
    {
        BlockEntity,
        This,
        Killer,
        KillerPlayer
    }

    public enum Source
    {
        This,
        Killer,
        KillerPlayer
    }

}