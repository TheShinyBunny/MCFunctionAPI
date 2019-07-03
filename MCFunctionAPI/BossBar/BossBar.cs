using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.BossBar
{
    public class BossBar
    {

        private ResourceLocation id;

        public BossBar(ResourceLocation id)
        {
            this.id = id;
        }

        public void Create(TextComponent displayName)
        {
            FunctionWriter.Write($"bossbar add {this} {displayName}");
        }

        public static implicit operator BossBar(ResourceLocation id)
        {
            return new BossBar(id);
        }

        public TextComponent Name {
            set
            {
                FunctionWriter.Write($"bossbar set {this} name {value}");
            }
        }
        public ChatColor Color
        {
            set
            {
                FunctionWriter.Write($"bossbar set {this} color {value}");
            }
        }
        public BossStyle Style
        {
            set
            {
                FunctionWriter.Write($"bossbar set {this} style {value}");
            }
        }
        public int Value
        {
            get
            {
                FunctionWriter.Write($"bossbar get {this} value");
                return 0;
            }
            set
            {
                FunctionWriter.Write($"bossbar set {this} value {value}");
            }
        }
        public int Max
        {
            get
            {
                FunctionWriter.Write($"bossbar get {this} max");
                return 100;
            }
            set
            {
                FunctionWriter.Write($"bossbar set {this} max {value}");
            }
        }
        public EntitySelector Players
        {
            get
            {
                FunctionWriter.Write($"bossbar get {this} players");
                return null;
            }
            set
            {
                FunctionWriter.Write($"bossbar set {this} players {value}");
            }
        }
        public bool Visible
        {
            get
            {
                FunctionWriter.Write($"bossbar get {this} visible");
                return true;
            }
            set
            {
                FunctionWriter.Write($"bossbar set {this} visible {value.ToString().ToLower()}");
            }
        }

        public void Remove()
        {
            FunctionWriter.Write($"bossbar remove {this}");
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
