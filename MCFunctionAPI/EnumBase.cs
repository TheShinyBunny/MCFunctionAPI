using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public abstract class EnumBase
    {

        public string id { get; }

        public EnumBase(string id)
        {
            this.id = id;
        }

        protected static T Get<T>(string id, IDictionary<string, T> registry) where T : EnumBase
        {
            return registry.TryGetValue(id, out T value) ? value : null;
        }

        public override string ToString()
        {
            return id;
        }

        public override bool Equals(object obj)
        {
            return obj is EnumBase @base &&
                   id == @base.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<string>.Default.GetHashCode(id);
        }


    }
}
