using MCFunctionAPI.Blocks;
using MCFunctionAPI.BossBar;
using MCFunctionAPI.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public abstract class EnumBase : INBTSerializable
    {

        public string Id { get; }

        public EnumBase(string id)
        {
            this.Id = id;
        }

        protected static T Get<T>(string id, IDictionary<string, T> registry) where T : EnumBase
        {
            return registry.TryGetValue(id, out T value) ? value : null;
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            return obj is EnumBase @base &&
                   Id == @base.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<string>.Default.GetHashCode(Id);
        }

        public object ToNBT()
        {
            return Id;
        }


        private static Type[] builtins = new Type[]
        {
            typeof(Color), typeof(ChatColor), typeof(CloneMode), typeof(EnumFacing), typeof(FillMode), typeof(Alignment), typeof(MaskMode),
            typeof(SetMode), typeof(BossStyle), typeof(Anchor), typeof(Effect), typeof(EntityType), typeof(EquipmentSlot), typeof(Gamemode),
            typeof(SoundSource), typeof(Difficulty), typeof(Dimension), typeof(Enchantment)
        };

        public static IDictionary GetRegistry(BuiltinEnums e)
        {
            Type t = builtins[(int)e];
            Console.WriteLine("getting builtin enum " + t);
            FieldInfo f = t.GetField("Registry",BindingFlags.Static | BindingFlags.NonPublic);
            if (f != null)
            {
                Console.WriteLine("found registry");
                Console.WriteLine(f.GetValue(null));
                return f.GetValue(null) as IDictionary;
            }
            return null;
        }
    }

    public enum BuiltinEnums
    {
        Color,
        ChatColor,
        CloneMode,
        EnumFacing,
        FillMode,
        Alignment,
        MaskMode,
        SetMode,
        BossStyle,
        Anchor,
        Effect,
        EntityType,
        EquipmentSlot,
        Gamemode,
        SoundSource,
        Difficulty,
        Dimension,
        Enchantment
    }
}
