namespace MCFunctionAPI.LootTables
{
    public abstract class IFunctionable<This> : IConditionable<This> where This : IFunctionable<This>
    {

        public This SetCount(IntRange count)
        {
            AddFunction(LootFunction.SetCount(count));
            return this as This;
        }

        public This LootingBonus(IntRange count)
        {
            AddFunction(LootFunction.LootingEnchant(count));
            return this as This;
        }

        public abstract void AddFunction(LootFunction f);

    }
}