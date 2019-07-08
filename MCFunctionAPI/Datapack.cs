using MCFunctionAPI.LootTables;
using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{

    /// <summary>
    /// An abstract class used to initialize a new datapack. extend and override the methods <see cref="GetName"/> and <see cref="GetDescription"/>.
    /// </summary>
    /// <example>
    /// Datapack basic definition:
    /// <code>
    /// class MyDatapack : Datapack
    /// {
    ///     public override string GetName() {
    ///         return "test";
    ///     }
    ///     
    ///     public override string GetDescription() {
    ///         return "Does things";
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class Datapack
    {

        private Namespace MinecraftNamespace;
        private FunctionTag TickTag;
        private FunctionTag LoadTag;

        /// <summary>
        /// The default namespace used for this datapack. Assigned using the constructor <see cref="Datapack(string)"/>
        /// </summary>
        public Namespace Namespace { get; }

        /// <summary>
        /// The folder contains the namespaces of this Datapack
        /// </summary>
        public DirectoryInfo DataFolder { get; }
        

        /// <summary>
        /// Creates a new datapack in the bin/out/ directory with a pack.mcmeta file, using the abstract methods.
        /// </summary>
        public Datapack()
        {
            DataFolder = Directory.CreateDirectory("out/" + GetName() + "/data");
            File.WriteAllText("out/" + GetName() + "/pack.mcmeta", 
                new NBT().Set("pack", new NBT().Set("description", GetDescription()).Set("pack_format", 1)).ToString(true, true));

            PreInit();
        }

        /// <summary>
        /// Creates a new datapack in the bin/out/ directory with a pack.mcmeta file and a namespace for the datapack's resources.
        /// </summary>
        /// <param name="mainNamespace">The default namespace to add resources to. Used to add suclasses of <see cref="FunctionContainer"/>s.</param>
        public Datapack(string mainNamespace) : this()
        {
            Namespace = CreateNamespace(mainNamespace);
            Init();
            foreach (Type t in this.GetType().GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                if (t.IsSubclassOf(typeof(FunctionContainer)))
                {
                    Namespace.AddFunctions(t);
                }
            }
            PostInit();
        }

        /// <summary>
        /// Called right after the pack.mcmeta file is generated.
        /// </summary>
        protected virtual void PreInit()
        {

        }

        /// <summary>
        /// Called right after the default namespace is created. (only when using constructor <see cref="Datapack(string)"/>)
        /// </summary>
        protected virtual void Init()
        {

        }

        /// <summary>
        /// Called after the nested function containers has been compiled. (only when using constructor <see cref="Datapack(string)"/>)
        /// </summary>
        protected virtual void PostInit()
        {

        }

        /// <summary>
        /// The Datapack's name used as the folder name.
        /// </summary>
        /// <returns>A name for the datapack</returns>
        public abstract string GetName();

        /// <summary>
        /// The description used in pack.mcmeta
        /// </summary>
        /// <returns>The datapack's description</returns>
        public abstract string GetDescription();

        public Namespace CreateNamespace(string name)
        {
            return new Namespace(this,name);
        }

        /// <summary>
        /// Creates a function tag minecraft:load, and adds the specified function id to it.
        /// </summary>
        /// <param name="function">The function's ResourceLocation to run on reload</param>
        public void CreateLoadTag(ResourceLocation function)
        {
            if (function.Namespace.LoadFunction == null)
            {
                if (LoadTag == null)
                {
                    EnsureMCNamespace();
                    LoadTag = new FunctionTag(new ResourceLocation(MinecraftNamespace, "load"));
                }
                LoadTag.Add(function);
            }
        }

        /// <summary>
        /// Creates a function tag minecraft:tick, and adds the specified function id to it.
        /// </summary>
        /// <remarks>
        /// The specified function will run every tick.
        /// </remarks>
        /// <param name="function">The function's ResourceLocation to run every tick</param>
        public void CreateTickTag(ResourceLocation function)
        {
            if (TickTag == null)
            {
                EnsureMCNamespace();
                TickTag = new FunctionTag(new ResourceLocation(MinecraftNamespace, "tick"));
            }
            TickTag.Add(function);
        }

        private void EnsureMCNamespace()
        {
            if (MinecraftNamespace == null)
            {
                MinecraftNamespace = new Namespace(this, "minecraft");
            }
        }

        /// <summary>
        /// Adds a vanilla loot table to the vanilla minecraft namespace, to override the vanilla datapack.
        /// </summary>
        /// <param name="id">The name of the file you want to override.</param>
        /// <param name="table">The loot table to use</param>
        public void AddVanillaLootTable(LootTable table)
        {
            EnsureMCNamespace();
            MinecraftNamespace.AddLootTable(table);
        }
    }
}
