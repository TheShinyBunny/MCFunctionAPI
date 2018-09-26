using MCFunctionAPI.Blocks;
using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Namespace
    {

        public string Name { get; private set; }
        public string Path { get; private set; }
        public FunctionContainer Functions { get; private set; }

        public static Namespace DefaultNamespace = new Namespace("minecraft");
        public static List<Namespace> Namespaces = new List<Namespace>();

        public Namespace(Datapack dp, string name)
        {
            this.Name = name;
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

        public void CreateFunctions(FunctionContainer container)
        {
            FunctionWriter.GenerateFunctions(this,container);
            Functions = container;
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
    }
}
