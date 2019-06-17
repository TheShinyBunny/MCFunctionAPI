using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class TextComponent : INBTSerializable
    {

        private string plain;
        private List<Segment> segments;

        public TextComponent(params Segment[] segments)
        {
            this.segments = segments.ToList();
        }

        public TextComponent(string plain)
        {
            this.plain = plain;
        }

        public static TextComponent Of(params Segment[] segments)
        {
            return new TextComponent(segments);
        }

        public object ToNBT()
        {
            return segments;
        }

        public override string ToString()
        {
            if (plain != null) return plain;
            return segments.Count == 1 ? segments[0].ToString() : $"[{string.Join(",",segments)}]";
        }

        public static implicit operator TextComponent(string s)
        {
            return new TextComponent(s);
        }

        
    }

    public class Segment : NBT
    {

        public Segment(NBT nbt) : base(nbt)
        {

        }

        public Segment() : base()
        {

        }

        public static Segment Text(string text)
        {
            return (Segment)new Segment().Set("text", text);
        }

        public static Segment Translate(string key, params TextComponent[] with)
        {
            return (Segment)new Segment().Set("translate", key).Set("with",with.ToList());
        }

        public static Segment Selector(Entities selector)
        {
            return (Segment)new Segment().Set("selector", selector.ToString());
        }

        public static Segment KeyBind(string key)
        {
            return (Segment)new Segment().Set("keybind", key);
        }

        public Segment Color(Color color)
        {
            return (Segment)Set("color", color.id);
        }

        public Segment Bold()
        {
            return (Segment)Set("bold", true);
        }

        public Segment Italic()
        {
            return (Segment)Set("italic", true);
        }

        public Segment Underline ()
        {
            return (Segment)Set("underline", true);
        }

        public Segment StrikeThrough()
        {
            return (Segment)Set("strikethrough", true);
        }

        public Segment Obfuscated()
        {
            return (Segment)Set("obfuscated", true);
        }

        public Segment Insertion(string insert)
        {
            return (Segment)Set("insertion", insert);
        }

        public Segment OpenURL(string url)
        {
            return (Segment)Set("clickEvent", new Segment().Set("action","open_url").Set("value",url));
        }

        public Segment RunCommand(string cmd)
        {
            return (Segment)Set("clickEvent", new Segment().Set("action", "run_command").Set("value", cmd));
        }

        public Segment Trigger(Objective trigger)
        {
            return RunCommand($"/trigger {trigger}");
        }

        public Segment ChangePage(int page)
        {
            return (Segment)Set("clickEvent", new Segment().Set("action", "change_page").Set("value", page + ""));
        }

        public Segment SuggestCommand(string cmd)
        {
            return (Segment)Set("clickEvent", new Segment().Set("action", "suggest_command").Set("value", cmd));
        }

        public Segment ShowText(string text)
        {
            return (Segment)Set("hoverEvent", new Segment().Set("action", "show_text").Set("value", text));
        }

        public Segment ShowItem(Item item)
        {
            return (Segment)Set("hoverEvent", new Segment().Set("action", "show_item").Set("value", item.ToNBT().ToString()));
        }

        public Segment ShowEntity(NBT entity)
        {
            return (Segment)Set("hoverEvent", new Segment().Set("action", "show_text").Set("value", entity.ToString()));
        }

        public override string ToString()
        {
            return base.ToString(true, false);
        }
    }
}
