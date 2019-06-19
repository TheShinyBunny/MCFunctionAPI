using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity.Models
{
    public class EntityModelBase<T> : EntityModel where T : EntityModelBase<T>
    {

        private T This;

        public EntityModelBase() : base("_internal_error", null)
        {
            This = (T)this;
        }

        public T Set(string key, object obj)
        {
            nbt.SetAny(key, obj);
            return This;
        }

        public T With(string tag, EntityType type)
        {
            this.tag = tag;
            this.type = type;
            return This;
        }


        public T Invulnerable()
        {
            return Set("Invulnerable", true);
        }

        public T CustomName(TextComponent name)
        {
            return Set("CustomName", name.ToString());
        }

        public T NoGravity()
        {
            return Set("NoGravity", true);
        }
    }
}
