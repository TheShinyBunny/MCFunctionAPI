using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity.Models
{
    public class Creeper : MobModel<Creeper>
    {

        public Creeper SetPowered()
        {
            Set("powered", true);
            return this;
        }

        public Creeper ExplosionRadius(int radius)
        {
            Set("ExplosionRadius", radius);
            return this;
        }

        public Creeper SetFuse(int fuse)
        {
            Set("Fuse", fuse);
            return this;
        }

        public Creeper Ignite()
        {
            Set("ignited", true);
            return this;
        }

    }
}
