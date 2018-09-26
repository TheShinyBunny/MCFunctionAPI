using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCFunctionAPI.Entity;

namespace MCFunctionAPI.Scoreboard
{
    public class Team
    {

        public static readonly Team Any = new Team("!");
        public static readonly Team None = new Team("");

        private string name;
        public PushingRule CollisionRule
        {
            set
            {
                FunctionWriter.Write($"team modify {this} collisionRule {value}");
            }
        }
        public VisibilityRule NameTagVisibility
        {
            set
            {
                FunctionWriter.Write($"team modify {this} nametagVisibility {value}");
            }
        }
        public VisibilityRule DeathMessageVisibility
        {
            set
            {
                FunctionWriter.Write($"team modify {this} deathMessageVisibility {value}");
            }
        }
        public bool SeeFriendlyInvisibles
        {
            set
            {
                FunctionWriter.Write($"team modify {this} seeFriendlyInvisibles {value}");
            }
        }
        public bool FriendlyFire
        {
            set
            {
                FunctionWriter.Write($"team modify {this} friendlyFire {value}");
            }
        }
        public Color Color
        {
            set
            {
                FunctionWriter.Write($"team modify {this} color {value}");
            }
        }
        public TextComponent Prefix
        {
            set
            {
                FunctionWriter.Write($"team modify {this} prefix {value}");
            }
        }
        public TextComponent Suffix
        {
            set
            {
                FunctionWriter.Write($"team modify {this} suffix {value}");
            }
        }

        public Team(string name)
        {
            this.name = name;
        }

        public static implicit operator Team(string name)
        {
            return new Team(name);
        }

        public void Create(TextComponent displayName)
        {
            FunctionWriter.Write("team add " + this + " " + displayName);
        }

        public void Create()
        {
            FunctionWriter.Write("team add " + this);
        }

        public void Remove()
        {
            FunctionWriter.Write("team remove " + this);
        }

        public void Clear()
        {
            FunctionWriter.Write("team empty " + this);
        }

        public override string ToString()
        {
            return name;
        }

        public void Add(string entities)
        {
            FunctionWriter.Write($"team join {this} {entities}");
        }
    }

    public class VisibilityRule : EnumBase
    {

        private static Dictionary<string, VisibilityRule> Registry;

        public VisibilityRule(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, VisibilityRule>();
            Registry.Add(id, this);
        }

        public static implicit operator VisibilityRule(string id)
        {
            return Get(id, Registry);
        }

    }

    public class PushingRule : EnumBase
    {

        private static Dictionary<string, PushingRule> Registry;

        public PushingRule(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, PushingRule>();
            Registry.Add(id, this);
        }

        public static implicit operator PushingRule(string id)
        {
            return Get(id, Registry);
        }

    }
}
