namespace MCFunctionAPI.LootTables
{
    public abstract class IFunctionable<This> : IConditionable<This> where This : IFunctionable<This>
    {

        public This CopyTileName()
        {
            AddFunction(LootFunction.CopyName);
            return this as This;
        }

        public This CopyNBT(NBTSource source, params NBTOp[] ops)
        {
            AddFunction(LootFunction.CopyNBT(source, ops));
            return this as This;
        }

        public This EnchantRandomly(params Enchantment[] enchants)
        {
            AddFunction(LootFunction.EnchantRandomly(enchants));
            return this as This;
        }

        public This EnchantWithLevels(IntRange levels, bool treasure = false)
        {
            AddFunction(LootFunction.EnchantWithLevels(levels,treasure));
            return this as This;
        }

        public This ExplorationMap(string structure, string iconId, int zoom = 2, int searchRadius = 50, bool skipExistingChunks = true)
        {
            AddFunction(LootFunction.ExplorationMap(structure, iconId, zoom, searchRadius, skipExistingChunks));
            return this as This;
        }

        public This ExplosionDecay()
        {
            AddFunction(LootFunction.ExplosionDecay);
            return this as This;
        }

        public This Smelt()
        {
            AddFunction(LootFunction.FurnaceSmelt);
            return this as This;
        }

        public This FillPlayerHead(Source source)
        {
            AddFunction(LootFunction.FillPlayerHead(source));
            return this as This;
        }

        public This LimitCount(IntRange limit)
        {
            AddFunction(LootFunction.LimitCount(limit));
            return this as This;
        }

        /// <summary>
        /// Adjusts the stack size based on the level of the Looting enchantment on the killer entity.
        /// </summary>
        /// <param name="count">Amount of additional items per level of looting</param>
        /// <returns></returns>
        public This LootingBonus(IntRange count)
        {
            AddFunction(LootFunction.LootingEnchant(count));
            return this as This;
        }

        public This SetContents(params Item[] entries)
        {
            AddFunction(LootFunction.SetContents(entries));
            return this as This;
        }

        /// <summary>
        /// Sets the stack size of the item
        /// </summary>
        /// <param name="count">The stack size</param>
        /// <returns></returns>
        public This SetCount(IntRange count)
        {
            AddFunction(LootFunction.SetCount(count));
            return this as This;
        }

        public This SetDamage(IntRange damage)
        {
            AddFunction(LootFunction.SetDamage(damage));
            return this as This;
        }

        public This SetLore(TextComponent[] lore, Source @s, bool replace)
        {
            AddFunction(LootFunction.SetLore(lore,@s,replace));
            return this as This;
        }

        public This SetName(string name)
        {
            AddFunction(LootFunction.SetName(name));
            return this as This;
        }

        public This SetName(TextComponent name, Source @s)
        {
            AddFunction(LootFunction.SetName(name,@s));
            return this as This;
        }

        public This SetNBT(NBT nbt)
        {
            AddFunction(LootFunction.SetNBT(nbt));
            return this as This;
        }

        public abstract void AddFunction(LootFunction f);

    }
}