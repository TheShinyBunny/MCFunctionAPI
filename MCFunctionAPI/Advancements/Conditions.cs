using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{

    public class EntityCondition : INBTSerializable
    {

        public DistanceCondition Distance { get; set; }
        public EffectMapCondition Effects { get; set; }
        public LocationCondition Location { get; set; }
        public NBT NBT { get; set; }
        public EntityType Type { get; set; }

        public EntityCondition()
        {
            Distance = new DistanceCondition();
            Effects = new EffectMapCondition();
            Location = new LocationCondition();
        }

        public object ToNBT()
        {
            return new NBT().Set("distance", Distance).Set("effects", Effects).Set("location", Location).Set("nbt", NBT?.ToString()).Set("type", Type?.ToString());
        }
    }

    public class EffectMapCondition : INBTSerializable
    {
        public Dictionary<Effect, EffectCondition> Effects;

        public EffectMapCondition()
        {
            Effects = new Dictionary<Effect, EffectCondition>();
        }

        public EffectMapCondition Contains(Effect effect)
        {
            Effects.Add(effect, null);
            return this;
        }

        public EffectMapCondition Contains(Effect effect, EffectCondition condition)
        {
            Effects.Add(effect, condition);
            return this;
        }

        public object ToNBT()
        {
            NBT nbt = new NBT();
            foreach (KeyValuePair<Effect,EffectCondition> e in Effects)
            {
                nbt.Set(e.Key.id, e.Value);
            }
            return nbt;
        }
    }

    public class EffectCondition : INBTSerializable
    {

        public IntRange Amplifier { get; set; }
        public IntRange Duration { get; set; }

        public EffectCondition(IntRange amplifier, IntRange duration)
        {
            Amplifier = amplifier;
            Duration = duration;
        }

        public EffectCondition()
        {

        }

        public object ToNBT()
        {
            return new NBT().Set("amplifier", Amplifier).Set("duration", Duration);
        }
    }

    public class LocationCondition : INBTSerializable
    {
        public string Biome { get; set; }
        public Dimension Dimension { get; set; }
        public string Feature { get; set; }

        public object ToNBT()
        {
            return new NBT().Set("biome", Biome).Set("dimension", Dimension?.id).Set("feature", Feature);
        }
    }

    public class PositionCondition : INBTSerializable
    {
        public DoubleRange X { get; set; }
        public DoubleRange Y { get; set; }
        public DoubleRange Z { get; set; }

        public virtual object ToNBT()
        {
            return new NBT().Set("x", X).Set("y", Y).Set("z", Z);
        }
    }

    public class DistanceCondition : PositionCondition
    {

        public DoubleRange Absolute { get; set; }
        public DoubleRange Horizontal { get; set; }

        public override object ToNBT()
        {
            return ((NBT)base.ToNBT()).Set("absolute", Absolute).Set("horizontal", Horizontal);
        }
    }

    public class Potion
    {

        public void Is(string potionId)
        {

        }

    }

    public class DimensionChange
    {

        private Dimension From { get; set; }
        private Dimension To { get; set; }

        public NBT ToNBT()
        {
            return new NBT().Set("from", From?.id).Set("to", To?.id);
        }
    }

    public class ItemCondition : INBTSerializable
    {
        public IntRange Count { get; set; }
        public IntRange Durability { get; set; }
        public EnchantmentPredicate[] Enchantments { get; set; }
        public Item Item { get; set; }
        public ResourceLocation Potion { get; set; }

        public ItemCondition Is(Item item)
        {
            Item = item;
            return this;
        }

        public ItemCondition EnchantedWith(params EnchantmentPredicate[] ench)
        {
            Enchantments = ench;
            return this;
        }

        public object ToNBT()
        {
            NBT nbt = new NBT().Set("count", Count).Set("durability", Durability).Set("item", Item?.Id.ToString()).Set("nbt",Item?.nbt.ToString()).Set("potion", Potion?.ToString()).Set("enchantments",Enchantments);
            return nbt;
        }
    }

    public class DamageCondition
    {
        public bool Blocked { get; set; }
        public DoubleRange Dealt { get; set; }
        public EntityCondition DirectEntity { get; set; }
        public EntityCondition SourceEntity { get; set; }
        public DoubleRange Taken { get; set; }
        public DamageTypeCondition Type { get; set; }

    }

    public class DamageTypeCondition
    {
        
        public bool BypassesArmor { get; set; }
        public bool BypassesInvulnerability { get; set; }
        public bool BypassesMagic { get; set; }
        public EntityCondition DirectEntity;
        public bool IsExplosion { get; set; }
        public bool IsFire { get; set; }
        public bool IsMagic { get; set; }
        public bool IsProjectile { get; set; }
        public EntityCondition SourceEntity { get; set; }

    }


    public class SlotsCondition
    {
        public IntRange Empty { get; set; }
        public IntRange Full { get; set; }
        public IntRange Occupied { get; set; }
    }
}
