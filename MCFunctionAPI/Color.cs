using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class Color : EnumBase
    {

        public static Color Black = new Color("black");
        public static Color White = new Color("white");
        public static Color DarkBlue = new Color("dark_blue");
        public static Color DarkGreen = new Color("dark_green");
        public static Color DarkAqua = new Color("dark_aqua");
        public static Color DarkRed = new Color("dark_red");
        public static Color DarkPurple = new Color("dark_purple");
        public static Color Gold = new Color("gold");
        public static Color Gray = new Color("gray");
        public static Color DarkGray = new Color("dark_gray");
        public static Color Blue = new Color("blue");
        public static Color Green = new Color("green");
        public static Color Aqua = new Color("aqua");
        public static Color Red = new Color("red");
        public static Color LightPurple = new Color("light_purple");
        public static Color Yellow = new Color("yellow");
        public static Color Reset = new Color("reset");

        private static IDictionary<string, Color> Registry;

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
