using MCFunctionAPI.Advancements;
using MCFunctionAPI.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class EntityType : EnumBase
    {
        

        private static IDictionary<string, EntityType> Registry;
        public static EntityType Item = new EntityType("item");
        public static EntityType ExperienceOrb = new EntityType("experience_orb");
        public static EntityType AreaEffectCloud = new EntityType("area_effect_cloud");
        public static EntityType ElderGuardian = new EntityType("elder_guardian");
        public static EntityType WitherSkeleton = new EntityType("wither_skeleton");
        public static EntityType Stray = new EntityType("stray");
        public static EntityType Egg = new EntityType("egg");
        public static EntityType LeashKnot = new EntityType("leash_knot");
        public static EntityType Painting = new EntityType("painting");
        public static EntityType Arrow = new EntityType("arrow");
        public static EntityType Snowball = new EntityType("snowball");
        public static EntityType Fireball = new EntityType("fireball");
        public static EntityType SmallFireball = new EntityType("small_fireball");
        public static EntityType EnderPearl = new EntityType("ender_pearl");
        public static EntityType EyeOfEnder = new EntityType("eye_of_ender");
        public static EntityType Potion = new EntityType("potion");
        public static EntityType ExperienceBottle = new EntityType("experience_bottle");
        public static EntityType ItemFrame = new EntityType("item_frame");
        public static EntityType WitherSkull = new EntityType("wither_skull");
        public static EntityType TNT = new EntityType("tnt");
        public static EntityType FallingBlock = new EntityType("falling_block");
        public static EntityType FireworkRocket = new EntityType("firework_rocket");
        public static EntityType Husk = new EntityType("husk");
        public static EntityType SpectralArrow = new EntityType("spectral_arrow");
        public static EntityType ShulkerBullet = new EntityType("shulker_bullet");
        public static EntityType DragonFireball = new EntityType("dragon_fireball");
        public static EntityType ZombieVillager = new EntityType("zombie_villager");
        public static EntityType SkeletonHorse = new EntityType("skeleton_horse");
        public static EntityType ZombieHorse = new EntityType("zombie_horse");
        public static EntityType ArmorStand = new EntityType("armor_stand");
        public static EntityType Donkey = new EntityType("donkey");
        public static EntityType Mule = new EntityType("mule");
        public static EntityType EvokerFangs = new EntityType("evoker_fangs");
        public static EntityType Evoker = new EntityType("evoker");
        public static EntityType Vex = new EntityType("vex");
        public static EntityType Vindicator = new EntityType("vindicator");
        public static EntityType Illusioner = new EntityType("illusioner");
        public static EntityType CommandBlockMinecart = new EntityType("command_block_minecart");
        public static EntityType Boat = new EntityType("boat");
        public static EntityType Minecart = new EntityType("minecart");
        public static EntityType ChestMinecart = new EntityType("chest_minecart");
        public static EntityType FurnaceMinecart = new EntityType("furnace_minecart");
        public static EntityType TntMinecart = new EntityType("tnt_minecart");
        public static EntityType HopperMinecart = new EntityType("hopper_minecart");
        public static EntityType SpawnerMinecart = new EntityType("spawner_minecart");
        public static EntityType Creeper = new EntityType("creeper");
        public static EntityType Skeleton = new EntityType("skeleton");
        public static EntityType Spider = new EntityType("spider");
        public static EntityType Giant = new EntityType("giant");
        public static EntityType Zombie = new EntityType("zombie");
        public static EntityType Slime = new EntityType("slime");
        public static EntityType Ghast = new EntityType("ghast");
        public static EntityType ZombiePigman = new EntityType("zombie_pigman");
        public static EntityType Enderman = new EntityType("enderman");
        public static EntityType CaveSpider = new EntityType("cave_spider");
        public static EntityType Silverfish = new EntityType("silverfish");
        public static EntityType Blaze = new EntityType("blaze");
        public static EntityType MagmaCube = new EntityType("magma_cube");
        public static EntityType EnderDragon = new EntityType("ender_dragon");
        public static EntityType Wither = new EntityType("wither");
        public static EntityType Bat = new EntityType("bat");
        public static EntityType Witch = new EntityType("witch");
        public static EntityType Endermite = new EntityType("endermite");
        public static EntityType Guardian = new EntityType("guardian");
        public static EntityType Shulker = new EntityType("shulker");
        public static EntityType Pig = new EntityType("pig");
        public static EntityType Sheep = new EntityType("sheep");
        public static EntityType Cow = new EntityType("cow");
        public static EntityType Chicken = new EntityType("chicken");
        public static EntityType Squid = new EntityType("squid");
        public static EntityType Wolf = new EntityType("wolf");
        public static EntityType Mooshroom = new EntityType("mooshroom");
        public static EntityType SnowGolem = new EntityType("snow_golem");
        public static EntityType Ocelot = new EntityType("ocelot");
        public static EntityType IronGolem = new EntityType("iron_golem");
        public static EntityType Horse = new EntityType("horse");
        public static EntityType Rabbit = new EntityType("rabbit");
        public static EntityType PolarBear = new EntityType("polar_bear");
        public static EntityType Llama = new EntityType("llama");
        public static EntityType LlamaSpit = new EntityType("llama_spit");
        public static EntityType Parrot = new EntityType("parrot");
        public static EntityType Villager = new EntityType("villager");
        public static EntityType EndCrystal = new EntityType("end_crystal");
        public static EntityType Turtle = new EntityType("turtle");
        public static EntityType Phantom = new EntityType("phantom");
        public static EntityType Trident = new EntityType("trident");
        public static EntityType Cod = new EntityType("cod");
        public static EntityType Salmon = new EntityType("salmon");
        public static EntityType Pufferfish = new EntityType("pufferfish");
        public static EntityType TropicalFish = new EntityType("tropical_fish");
        public static EntityType Drowned = new EntityType("drowned");
        public static EntityType Dolphin = new EntityType("dolphin");
        public static EntityType LingeringPotion = new EntityType("lingering_potion");
        public static EntityType FishingBobber = new EntityType("fishing_bobber");
        public static EntityType LightningBolt = new EntityType("lightning_bolt");
        public static EntityType Player = new EntityType("player");
        public static EntityType TippedArrow = new EntityType("tipped_arrow");
        public static IEnumerable<EntityType> All
        {
            get
            {
                return Registry.Values;
            }
        }

        public EntityType(string id) : base(id)
        {
            if (Registry == null)
            {
                Registry = new Dictionary<string, EntityType>();
            }
            Registry.Add(id, this);
        }

        public static implicit operator EntityType(string id)
        {

            return Get(id, Registry);
        }
        

    }
}
