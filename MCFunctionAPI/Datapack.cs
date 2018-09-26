using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public abstract class Datapack
    {

        private Namespace DefaultNamespace;

        public DirectoryInfo DataFolder { get; }
        

        public Datapack()
        {
            DataFolder = Directory.CreateDirectory("Tests/" + GetName() + "/data");
        }

        public abstract string GetName();

        public abstract string GetDescription();

        public Namespace CreateNamespace(string name)
        {
            return new Namespace(this,name);
        }

        public FunctionTag CreateLoadTag(ResourceLocation function)
        {
            EnsureDefaultNamespace();
            FunctionTag tag = new FunctionTag(new ResourceLocation(DefaultNamespace,"load"));
            tag.Add(function);
            return tag;
        }

        public FunctionTag CreateTickTag(ResourceLocation function)
        {
            EnsureDefaultNamespace();
            FunctionTag tag = new FunctionTag(new ResourceLocation(DefaultNamespace, "tick"));
            tag.Add(function);
            return tag;
        }

        private void EnsureDefaultNamespace()
        {
            if (DefaultNamespace == null)
            {
                DefaultNamespace = new Namespace(this, "minecraft");
            }
        }
    }
}
