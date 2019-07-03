using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class ChatColor : EnumBase
    {

        public static ChatColor Black = new ChatColor("black");
        public static ChatColor White = new ChatColor("white");
        public static ChatColor DarkBlue = new ChatColor("dark_blue");
        public static ChatColor DarkGreen = new ChatColor("dark_green");
        public static ChatColor DarkAqua = new ChatColor("dark_aqua");
        public static ChatColor DarkRed = new ChatColor("dark_red");
        public static ChatColor DarkPurple = new ChatColor("dark_purple");
        public static ChatColor Gold = new ChatColor("gold");
        public static ChatColor Gray = new ChatColor("gray");
        public static ChatColor DarkGray = new ChatColor("dark_gray");
        public static ChatColor Blue = new ChatColor("blue");
        public static ChatColor Green = new ChatColor("green");
        public static ChatColor Aqua = new ChatColor("aqua");
        public static ChatColor Red = new ChatColor("red");
        public static ChatColor LightPurple = new ChatColor("light_purple");
        public static ChatColor Yellow = new ChatColor("yellow");
        public static ChatColor Reset = new ChatColor("reset");

        private static IDictionary<string, ChatColor> Registry;

        public static IEnumerable<ChatColor> Values => Registry.Values;

        public ChatColor(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, ChatColor>();
            Registry.Add(id, this);
        }

        public static implicit operator ChatColor(string s)
        {
            return Get(s, Registry);
        }
    }

    public class Color : EnumBase
    {
        public static Color Black = new Color("black");
        public static Color White = new Color("white");
        public static Color Blue = new Color("blue");
        public static Color Green = new Color("green");
        public static Color Cyan = new Color("cyan");
        public static Color Red = new Color("red");
        public static Color Purple = new Color("purple");
        public static Color Orange = new Color("orange");
        public static Color Gray = new Color("gray");
        public static Color LightGray = new Color("light_gray");
        public static Color LightBlue = new Color("light_blue");
        public static Color Lime = new Color("lime");
        public static Color Brown = new Color("brown");
        public static Color Pink = new Color("pink");
        public static Color Magenta = new Color("magenta");
        public static Color Yellow = new Color("yellow");

        private static IDictionary<string, Color> Registry;

        public static IEnumerable<Color> Values => Registry.Values;

        public Color(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Color>();
            Registry.Add(id, this);
        }

        public static implicit operator Color(string s)
        {
            return Get(s, Registry);
        }
    }
}
