using MCFunctionAPI.Blocks;
using MCFunctionAPI.LootTables;
using MCFunctionAPI.Scoreboard;
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
    public class Namespace
    {
        /// <summary>
        /// The name of this Namespace
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The datapack containing this namespace
        /// </summary>
        public Datapack Datapack { get; }

        /// <summary>
        /// The full path to save this namespace's data
        /// </summary>
        public string Path { get; private set; }

        public static Namespace DefaultNamespace = new Namespace("minecraft");
        public static List<Namespace> Namespaces = new List<Namespace>();

        public string LoadFunctionPath { get; set; }
        public string TickFunctionPath { get; set; }
        public List<Objective> LoadObjectives = new List<Objective>();

        public Dictionary<ScoreEventHandler,string> PendingScoreHandlers = new Dictionary<ScoreEventHandler,string>();

        public Namespace(Datapack dp, string name)
        {
            this.Name = name;
            this.Datapack = dp;
            this.Path = dp.DataFolder.FullName + "/" + name;
            Directory.CreateDirectory(Path);
            Namespaces.Add(this);
        }

        private Namespace(string name)
        {
            Name = name;
        }

        public static implicit operator Namespace(string name)
        {
            if (name.EqualsIgnoreCase("minecraft")) return DefaultNamespace;
            foreach (var ns in Namespaces)
            {
                if (ns.Name.EqualsIgnoreCase(name))
                {
                    return ns;
                }
            }
            return new Namespace(name);
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Creates functions to this namespace from the specified container.
        /// </summary>
        /// <param name="container"></param>
        public void AddFunctions(Type container)
        {
            FunctionWriter.GenerateFunctions(container.GetCustomAttribute<Root>() == null ? FunctionWriter.LowerCase(container.Name) + "/" : "",this, container);
        }

        public BlockTag AddBlockTag(string id, params Block[] values)
        {
            BlockTag tag = new BlockTag(new ResourceLocation(this, id));
            foreach (Block b in values)
            {
                tag.Add(b);
            }
            return tag;
        }

        public ItemTag AddItemTag(string id, params Item[] values)
        {
            ItemTag tag = new ItemTag(new ResourceLocation(this, id));
            foreach (Item i in values)
            {
                tag.Add(i);
            }
            return tag;
        }

        public FunctionTag AddFunctionTag(string id, params ResourceLocation[] values)
        {
            FunctionTag tag = new FunctionTag(new ResourceLocation(this, id));
            foreach (ResourceLocation rl in values)
            {
                tag.Add(rl);
            }
            return tag;
        }

        public void AddLoadObjective(Objective objective)
        {
            if (LoadFunctionPath == null)
            {
                LoadObjectives.Add(objective);
            }
            else
            {
                File.AppendAllLines(Path + "/functions/" + LoadFunctionPath + ".mcfunction", new string[] { $"scoreboard objectives add {objective.Name} dummy" });
            }
        }

        /// <summary>
        /// Adds a loot table to this namespace.
        /// </summary>
        /// <param name="id">The name of the file</param>
        /// <param name="table">The loot table to add</param>
        public void AddLootTable(LootTable table)
        {
            string dir = GetLootTableTypeDir(table.Type);
            if (dir != null)
            {
                string path = Path + "/loot_tables/" + dir;
                Directory.CreateDirectory(path);
                File.WriteAllText(path + "/" + table.Name + ".json", table.ToJson());
            }
        }

        public static string GetLootTableTypeDir(TableType type)
        {
            switch (type)
            {
                case TableType.Block:
                    return "blocks";
                case TableType.Entity:
                    return "entities";
                case TableType.Fishing:
                    return "gameplay/fishing";
                case TableType.Gift:
                    return "gameplay";
                case TableType.Chest:
                    return "chests";
            }
            return null;
        }
    }
}
