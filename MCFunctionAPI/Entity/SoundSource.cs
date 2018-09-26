using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class SoundSource : EnumBase
    {

        public static SoundSource Master = new SoundSource("master");
        public static SoundSource Music = new SoundSource("music");
        public static SoundSource Record = new SoundSource("record");
        public static SoundSource Weather = new SoundSource("weather");
        public static SoundSource Block = new SoundSource("block");
        public static SoundSource Hostile = new SoundSource("hostile");
        public static SoundSource Neutral = new SoundSource("neutral");
        public static SoundSource Player = new SoundSource("player");
        public static SoundSource Ambient = new SoundSource("ambient");
        public static SoundSource Voice = new SoundSource("voice");

        private static IDictionary<string, SoundSource> Registry;

        public SoundSource(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, SoundSource>();
            Registry.Add(id, this);
        }

        public static implicit operator SoundSource(string id)
        {
            return Get(id, Registry);
        }

    }
}
