using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class ResourceLocation : ITaggable, INBTSerializable
    {

        public Namespace Namespace { get; set; }
        public string Path { get; set; }
        public bool tag;

        public ResourceLocation Id => this;

        public ResourceLocation(Namespace ns, string path) : this(false,ns,path)
        {

        }

        public ResourceLocation(bool tag, Namespace ns, string path)
        {
            Namespace = ns;
            Path = path;
            this.tag = tag;
        }

        public ResourceLocation(string s)
        {
            ResourceLocation loc = Of(s);
            tag = loc.tag;
            Namespace = loc.Namespace;
            Path = loc.Path;
        }

        public static ResourceLocation Of(string s)
        {
            bool tag = s[0] == '#';
            int i = s.IndexOf(":");
            if (i == -1)
            {
                return new ResourceLocation(tag, "minecraft", s.Substring(tag ? 1 : 0));
            }
            return new ResourceLocation(tag, s.Substring(tag ? 1 : 0, i + (tag ? -1 : 0)), s.Substring(i + 1));
        }

        public static implicit operator ResourceLocation(string s)
        {
            return Of(s);
        }

        public override string ToString()
        {
            return $"{(tag ? "#":"")}{Namespace}:{Path}";
        }

        public object ToNBT()
        {
            return ToString();
        }
    }
}
