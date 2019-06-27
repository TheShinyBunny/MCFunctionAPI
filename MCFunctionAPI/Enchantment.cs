using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Enchantment : EnumBase
    {

        public static Enchantment Protection = new Enchantment("protection",4);
        public static Enchantment FireProtection = new Enchantment("fire_protection",4);
        public static Enchantment FeatherFalling = new Enchantment("feather_falling", 4);
        public static Enchantment BlastProtection = new Enchantment("blast_protection", 4);
        public static Enchantment ProjectileProtection = new Enchantment("projectile_protection", 4);
        public static Enchantment Thorns = new Enchantment("thorns",3);
        public static Enchantment Respiration = new Enchantment("respiration",3);
        public static Enchantment DepthStrider = new Enchantment("depth_strider",3);
        public static Enchantment AquaAffinity = new Enchantment("aqua_affinity");
        public static Enchantment Sharpness = new Enchantment("sharpness",5);
        public static Enchantment Smite = new Enchantment("smite", 5);
        public static Enchantment BaneOfArthropods = new Enchantment("bane_of_arthropods", 5);
        public static Enchantment Knockback = new Enchantment("knockback",2);
        public static Enchantment FireAspect = new Enchantment("fire_aspect",2);
        public static Enchantment Looting = new Enchantment("looting",3);
        public static Enchantment Efficiency = new Enchantment("efficiency", 5);
        public static Enchantment SilkTouch = new Enchantment("silk_touch");
        public static Enchantment Unbreaking = new Enchantment("unbreaking",3);
        public static Enchantment Fortune = new Enchantment("fortune",3);
        public static Enchantment Power = new Enchantment("power", 5);
        public static Enchantment Punch = new Enchantment("punch",2);
        public static Enchantment Flame = new Enchantment("flame");
        public static Enchantment Infinity = new Enchantment("infinity");
        public static Enchantment LuckOfTheSea = new Enchantment("luck_of_the_sea",3);
        public static Enchantment Lure = new Enchantment("lure",3);
        public static Enchantment FrostWalker = new Enchantment("frost_walker",2);
        public static Enchantment Mending = new Enchantment("mending");
        public static Enchantment CurseOfBinding = new Enchantment("binding_curse");
        public static Enchantment CurseOfVanishing = new Enchantment("vanishing_curse");
        public static Enchantment Impaling = new Enchantment("impaling",5);
        public static Enchantment Riptide = new Enchantment("riptide",3);
        public static Enchantment Loyalty = new Enchantment("loyalty",3);
        public static Enchantment Channeling = new Enchantment("channeling");
        public static Enchantment SweepingEdge = new Enchantment("sweeping",3);

        private static IDictionary<string, Enchantment> Registry;
        public int MaxLevel { get; }

        public Enchantment(string id) : this(id,1)
        {

        }

        public Enchantment(string id, int lvl) : base(id)
        {
            this.MaxLevel = lvl;
            if (Registry == null)
            {
                Registry = new Dictionary<string, Enchantment>();
            }
            Registry.Add(id, this);
        }

        public static implicit operator Enchantment(string id)
        {
            return Get(id, Registry);
        }

        public EnchantmentPredicate Levels(IntRange levels)
        {
            return new EnchantmentPredicate(this, levels);
        }

    }

    public class EnchantmentPredicate : INBTSerializable
    {
        private Enchantment ench;
        private IntRange levels;

        public EnchantmentPredicate(Enchantment ench, IntRange levels)
        {
            this.ench = ench;
            this.levels = levels;
        }

        public object ToNBT()
        {
            return new NBT().Set("enchantment", ench).Set("levels", levels);
        }
    }
}
