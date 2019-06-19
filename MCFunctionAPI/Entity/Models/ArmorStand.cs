using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity.Models
{
    public class ArmorStand : EntityModelBase<ArmorStand>
    {
        public ArmorStand()
        {
        }

        public ArmorStand Invisible()
        {
            return Set("Invisible", true);
        }

        public ArmorStand Marker()
        {
            return Set("Marker", true);
        }
    }
}
