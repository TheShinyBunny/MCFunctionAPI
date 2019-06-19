using MCFunctionAPI.LootTables;
using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{

    /// <summary>
    /// The Datapack class is used to define and create your datapack.
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

        private Namespace DefaultNamespace;
        private FunctionTag TickTag;
        private FunctionTag LoadTag;

        /// <summary>
        /// The folder contains the namespaces of this Datapack
        /// </summary>
        public DirectoryInfo DataFolder { get; }
        

        public Datapack()
        {
            DataFolder = Directory.CreateDirectory("out/" + GetName() + "/data");
            File.WriteAllText("out/" + GetName() + "/pack.mcmeta", 
                new NBT().Set("pack", new NBT().Set("description", GetDescription()).Set("pack_format", 1)).ToString(true, true));
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
            if (function.Namespace.LoadFunctionPath == null)
            {
                if (LoadTag == null)
                {
                    EnsureDefaultNamespace();
                    LoadTag = new FunctionTag(new ResourceLocation(DefaultNamespace, "load"));
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
                EnsureDefaultNamespace();
                TickTag = new FunctionTag(new ResourceLocation(DefaultNamespace, "tick"));
            }
            TickTag.Add(function);
        }

        private void EnsureDefaultNamespace()
        {
            if (DefaultNamespace == null)
            {
                DefaultNamespace = new Namespace(this, "minecraft");
            }
        }

        /// <summary>
        /// Adds a vanilla loot table to the vanilla minecraft namespace, to override the vanilla datapack.
        /// </summary>
        /// <param name="id">The name of the file you want to override.</param>
        /// <param name="table">The loot table to use</param>
        public void AddVanillaLootTable(LootTable table)
        {
            EnsureDefaultNamespace();
            DefaultNamespace.AddLootTable(table);
        }
    }
}
