using MCFunctionAPI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class ResourceLocation : ITaggable
    {

        public Namespace Namespace { get; set; }
        public string Path { get; set; }

        public ResourceLocation Id => this;

        public ResourceLocation(Namespace ns, string path)
        {
            Namespace = ns;
            Path = path;
        }

        public ResourceLocation(string s)
        {
            ResourceLocation loc = Of(s);
            Namespace = loc.Namespace;
            Path = loc.Path;
        }

        public static ResourceLocation Of(string s)
        {
            int i = s.IndexOf(":");
            if (i == -1)
            {
                return new ResourceLocation("minecraft", s);
            }
            return new ResourceLocation(s.Substring(0,i),s.Substring(i));
        }

        public static implicit operator ResourceLocation(string s)
        {
            return Of(s);
        }

        public override string ToString()
        {
            return $"{Namespace}:{Path}";
        }
    }
}
