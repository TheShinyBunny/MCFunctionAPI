using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class EffectInstance : INBTSerializable
    {

        public Effect Effect { get; set; }
        public int? Amplifier { get; set; }
        public int? Duration { get; set; }
        public bool? ShowParticles { get; set; }
        public bool? Ambient { get; set; }
        public bool? ShowIcon { get; set; }

        public EffectInstance(Effect effect, int? amplifier = null, int? duration = null, bool? showParticles = null, bool? ambient = null, bool? showIcon = null)
        {
            Effect = effect;
            Amplifier = amplifier;
            Duration = duration;
            ShowParticles = showParticles;
            Ambient = ambient;
            ShowIcon = showIcon;
        }

        public object ToNBT()
        {
            return new NBT().Set("Id", Effect.id).Set("Amplifier", Amplifier).Set("Duration", Duration).Set("Ambient", Ambient).Set("ShowParticles", ShowParticles).Set("ShowIcon", ShowIcon);
        }
    }
}
