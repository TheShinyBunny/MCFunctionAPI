using MCFunctionAPI.Advancements;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class EntitySelector : Entities
    {

        private Target target;
        public DoubleRange X;
        public DoubleRange Y;
        public DoubleRange Z;
        public IntRange Distance;
        public DoubleRange DX;
        public DoubleRange DY;
        public DoubleRange DZ;
        public IntRange Level;
        public IntRange X_Rotation;
        public IntRange Y_Rotation;
        public ScoreSet Scores;
        public new TagSet Tags;
        public TeamArgument Team;
        public uint? Limit;
        public new GamemodeArgument Gamemode;
        public NameArgument Name;
        public TypeArgument Type;
        public Sort Sort;
        public new AdvancementsArgument Advancements;

        public EntitySelector(Target target)
        {
            this.target = target;
            Scores = new ScoreSet();
            Tags = new TagSet();
            Team = new TeamArgument();
            Name = new NameArgument();
            Type = new TypeArgument();
            Gamemode = new GamemodeArgument();
            Execute = new Execute();
            Advancements = new AdvancementsArgument();
        }

        public EntitySelector Tag(string tag)
        {
            Tags.And(tag);
            return this;
        }

        public EntitySelector NotTag(string tag)
        {
            Tags.Not(tag);
            return this;
        }

        public EntitySelector HasTags()
        {
            Tags.Any();
            return this;
        }

        public EntitySelector HasNoTags()
        {
            Tags.None();
            return this;
        }

        public static EntitySelector AllEntities()
        {
            return new EntitySelector(Entity.Target.AllEntities);
        }

        public static EntitySelector Target(Target t)
        {
            return new EntitySelector(t);
        }

        public bool CanTargetPlayers()
        {
            if (Type.not.Contains("player"))
            {
                return false;
            } else if (Type.Type != EntityType.Player)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            List<string> args = new List<string>();
            AddArg(args, nameof(X), X);
            AddArg(args, nameof(Y), Y);
            AddArg(args, nameof(Z), Z);
            AddArg(args, nameof(Distance), Distance);
            AddArg(args, nameof(DX), DX);
            AddArg(args, nameof(DY), DY);
            AddArg(args, nameof(DZ), DZ);
            AddArg(args, nameof(Level), Level);
            AddArg(args, nameof(X_Rotation), X_Rotation);
            AddArg(args, nameof(Y_Rotation), Y_Rotation);
            AddArg(args, nameof(Scores), Scores);
            AddArg(args, nameof(Tags), Tags);
            AddArg(args, nameof(Team), Team);
            if (Limit != null)
            {
                args.Add("limit=" + Limit);
            }
            if (Sort != null)
            {
                args.Add("sort=" + Sort);
            }
            AddArg(args, nameof(Gamemode), Gamemode);
            AddArg(args, nameof(Name), Name);
            AddArg(args, nameof(Type), Type);
            AddArg(args, nameof(Advancements), Advancements);

            string s = string.Join(",", args);
            if (args.Count > 0)
            {
                s = "[" + s + "]";
            }
            return target + s;
        }

        private void AddArg<T>(List<string> args, string name, SelectorArgument<T> arg)
        {
            if (arg != null && arg.ShouldAdd())
            {
                foreach (var k in arg.Keys)
                {
                    args.Add($"{name.ToLower()}={arg.BuildValue(k)}");
                }
            }
        }

        public Execute Execute { get; set; }

        public static implicit operator string(EntitySelector selector)
        {
            return selector.ToString();
        }

    }


    public abstract class SelectorArgument<Key>
    {
        public abstract string BuildValue(Key k);

        public abstract IEnumerable<Key> Keys { get; }

        public virtual bool ShouldAdd()
        {
            return true;
        }

    }

    public abstract class SingleArgument : SelectorArgument<int>
    {
        public override string BuildValue(int k)
        {
            return BuildValue();
        }

        public override IEnumerable<int> Keys => new int[] { 0 };

        public abstract string BuildValue();

    }

    public class TypeArgument : SelectorArgument<int>
    {

        public EntityType Type { get; private set; }

        public List<EntityType> not;

        public TypeArgument()
        {
            not = new List<EntityType>();
        }

        public TypeArgument(EntityType type)
        {
            this.Type = type;
        }

        public void Not(EntityType type)
        {
            not.Add(type);
        }

        public void Is(EntityType type)
        {
            this.Type = type;
        }

        public override string BuildValue(int k)
        {
            return Type == null ? "!" + not.ElementAt(k).id : Type.id;
        }

        public override IEnumerable<int> Keys => Type == null ? from t in not select not.IndexOf(t) : new int[] { 0 };

        public static implicit operator TypeArgument(EntityType type)
        {
            return new TypeArgument(type);
        }

        public static implicit operator TypeArgument(string typeId)
        {
            return new TypeArgument(typeId);
        }

    }

    public class AdvancementsArgument : SingleArgument
    {

        private Dictionary<Advancement, AdvancementValue> Advancements;

        public AdvancementsArgument()
        {
            Advancements = new Dictionary<Advancement, AdvancementValue>();
        }

        public override string BuildValue()
        {
            return $"{{{string.Join(",", from s in Advancements select s.Key + "=" + s.Value)}}}";
        }

        public AdvancementsArgument Done(Advancement a)
        {
            Advancements.Add(a, true);
            return this;
        }

        public AdvancementsArgument NotDone(Advancement a)
        {
            Advancements.Add(a, false);
            return this;
        }
        
        public AdvancementsArgument DoneSpecific(Advancement a, Dictionary<string,bool> Criteria)
        {
            Advancements.Add(a, Criteria);
            return this;
        }

        public override bool ShouldAdd()
        {
            return Advancements.Count != 0;
        }

        private class AdvancementValue
        {

            private bool b;
            private Dictionary<string, bool> Criteria;

            public AdvancementValue(bool b)
            {
                this.b = b;
            }

            public AdvancementValue(Dictionary<string, bool> criteria)
            {
                Criteria = criteria;
            }

            public override string ToString()
            {
                return Criteria == null ? b.ToString().ToLower() : $"{{{string.Join(",",from c in Criteria select c.Key + "=" + c.Value.ToString().ToLower())}}}";
            }

            public static implicit operator AdvancementValue(bool b)
            {
                return new AdvancementValue(b);
            }

            public static implicit operator AdvancementValue(Dictionary<string,bool> crit)
            {
                return new AdvancementValue(crit);
            }
        }
    }

    public class Sort : EnumBase
    {

        public static Sort Nearest = new Sort("nearest");
        public static Sort Furtherst = new Sort("furthest");
        public static Sort Random = new Sort("random");
        public static Sort Arbitrary = new Sort("arbitrary");

        private static Dictionary<string, Sort> Registry;

        public Sort(string id) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, Sort>();
            Registry.Add(id, this);
        }

        public static implicit operator Sort(string id)
        {
            return Get(id, Registry);
        }
    }

    public class GamemodeArgument : SelectorArgument<Gamemode>
    {
        private IDictionary<Gamemode, bool> gms;

        public GamemodeArgument(Gamemode gm) : base()
        {
            gms = new Dictionary<Gamemode, bool>();
            Is(gm);
        }

        public GamemodeArgument()
        {
            this.gms = new Dictionary<Gamemode, bool>();
        }

        public void Is(Gamemode gm)
        {
            this.gms.Add(gm, true);
        }

        public void IsNot(Gamemode gm)
        {
            this.gms.Add(gm, false);
        }

        public override string BuildValue(Gamemode gm)
        {
            if (gms.TryGetValue(gm, out bool b))
            {
                return $"{(b ? "" : "!")}{gm}";
            }
            return "error";
        }

        public override IEnumerable<Gamemode> Keys => gms.Keys;

        public static implicit operator GamemodeArgument(Gamemode gm)
        {
            return new GamemodeArgument(gm);
        }

        public static implicit operator GamemodeArgument(string name)
        {
            return new GamemodeArgument(name);
        }

        public static implicit operator GamemodeArgument(uint id)
        {
            return new GamemodeArgument(id);
        }
    }

    public class NameArgument : SingleArgument
    {

        private string name;
        private bool not;

        public NameArgument(string name)
        {
            this.name = name;
            not = false;
        }

        public NameArgument()
        {
            not = false;
        }

        public void Is(string name)
        {
            this.name = name;
        }

        public void IsNot(string name)
        {
            this.Is(name);
            not = true;
        }

        public override bool ShouldAdd()
        {
            return name != null;
        }

        public override string BuildValue()
        {
            return $"{(not ? "!" : "")}{name}";
        }

        public static implicit operator NameArgument(string name)
        {
            return new NameArgument(name);
        }

    }

    public abstract class NoneAnyMixin<Key> : SelectorArgument<Key>
    {
        protected bool any;
        protected bool none;

        public NoneAnyMixin()
        {
            any = false;
            none = false;
        }

        public void Any()
        {
            this.any = true;
        }

        public void None()
        {
            this.none = true;
        }
    }

    public class TeamArgument : NoneAnyMixin<int>
    {
        private Team team;
        private bool not;

        public TeamArgument(Team t)
        {
            this.team = t;
            not = false;
        }

        public TeamArgument()
        {
            not = false;
        }

        public void Is(Team t)
        {
            this.team = t;
        }

        public void IsNot(Team t)
        {
            this.team = t;
            not = true;
        }

        public override string BuildValue(int k)
        {
            return none ? "" : any ? "!" : not ? "!" + team : team.ToString();
        }

        public override IEnumerable<int> Keys
        {
            get
            {
                if (any || none || team != null)
                {
                    return new int[] { 0 };
                }
                return new int[0];
            }
        }

        public static implicit operator TeamArgument(string t)
        {
            return new TeamArgument(t);
        }

        public static implicit operator TeamArgument(Team t)
        {
            return new TeamArgument(t);
        }

    }

    public struct Target
    {

        public static Target AllEntities = "@e";
        public static Target AllPlayers = "@a";
        public static Target ClosestPlayer = "@p";
        public static Target RandomPlayer = "@r";
        public static Target Self = "@s";

        private static string[] valid;

        private string sign;

        private Target(string sign)
        {
            if (valid == null) {
                valid = new string[] { "@e", "@a", "@p", "@r", "@s" };
            }
            this.sign = sign;
        }

        public static implicit operator Target(string sign)
        {
            if (valid == null)
            {
                valid = new string[] { "@e", "@a", "@p", "@r", "@s" };
            }
            if (!sign.EqualsAny(valid))
            {
                throw new ArgumentException("Target must be one of: " + valid);
            }
            return new Target(sign);
        }

        public static bool operator ==(Target a, Target b)
        {
            return a.sign.Equals(b.sign);
        }

        public static bool operator !=(Target a, Target b)
        {
            return !a.sign.Equals(b.sign);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Target))
            {
                return false;
            }

            var target = (Target)obj;
            return sign == target.sign;
        }

        public override int GetHashCode()
        {
            return sign.GetHashCode();
        }

        public override string ToString()
        {
            return sign;
        }
    }

    public class ScoreSet : SingleArgument
    {
        private IDictionary<Objective, IntRange> Scores;

        public ScoreSet()
        {
            Scores = new Dictionary<Objective, IntRange>();
        }

        public override string BuildValue()
        {
            return $"{{{string.Join(",",from s in Scores select s.Key + "=" + s.Value)}}}";
        }

        public ScoreSet Where(Objective o, IntRange range)
        {
            Scores.Add(o, range);
            return this;
        }

        public override bool ShouldAdd()
        {
            return Scores.Count != 0;
        }

    }

    public class IntRange : SingleArgument
    {
        private int? min;
        private int? max;
        private int? exact;

        public IntRange(int? min, int? max)
        {
            this.min = min;
            this.max = max;
            this.exact = null;
        }

        public IntRange(int exact) : this(null,null)
        {
            this.exact = exact;
        }

        public static implicit operator IntRange(string s)
        {
            if (s.Contains(".."))
            {
                int index = s.IndexOf("..");
                string smin = s.Substring(0,index);
                int? min = null;
                if (smin != "")
                {
                    min = int.Parse(smin);
                }
                int? max = null;
                if (index + 2 < s.Length)
                {
                    string smax = s.Substring(index + 2);
                    if (smax != "")
                    {
                        max = int.Parse(smax);
                    }
                }
                return new IntRange(min, max);
            }
            return new IntRange(int.Parse(s));
        }

        public static implicit operator IntRange(int i)
        {
            return new IntRange(i);
        }

        public override string ToString()
        {
            string s = "";
            if (min != null)
                s += min;
            if (exact == null)
                s += "..";
            else
                s += exact;
            if (max != null)
                s += max;
            return s;
        }

        public override string BuildValue()
        {
            return ToString();
        }
    }

    public class DoubleRange : SingleArgument
    {
        private double? min;
        private double? max;
        private double? exact;

        public DoubleRange(double? min, double? max)
        {
            this.min = min;
            this.max = max;
            this.exact = null;
        }

        public DoubleRange(double exact) : this(null, null)
        {
            this.exact = exact;
        }

        public static implicit operator DoubleRange(string s)
        {
            if (s.Contains(".."))
            {
                int index = s.IndexOf("..");
                string smin = s.Substring(0, index);
                double? min = null;
                if (smin != "")
                {
                    min = double.Parse(smin);
                }
                double? max = null;
                if (index + 2 < s.Length)
                {
                    string smax = s.Substring(index + 2);
                    if (smax != "")
                    {
                        max = double.Parse(smax);
                    }
                }
                return new DoubleRange(min, max);
            }
            return new DoubleRange(double.Parse(s));
        }

        public static implicit operator DoubleRange(double d)
        {
            return new DoubleRange(d);
        }

        public override string ToString()
        {
            string s = "";
            if (min != null)
                s += min;
            if (exact == null)
                s += "..";
            else
                s += exact;
            if (max != null)
                s += max;
            return s;
        }

        public static DoubleRange Of(double min, int max)
        {
            return new DoubleRange(min, max);
        }

        public override string BuildValue()
        {
            return ToString();
        }
    }

    public class TagSet : NoneAnyMixin<string>
    {
        private IDictionary<string, bool> Tags;

        public TagSet() : base()
        {
            Tags = new Dictionary<string, bool>();
        }

        public override IEnumerable<string> Keys => Tags.Count == 0 ? none || any ? new string[] { "" } : new string[0] : Tags.Keys;

        public TagSet And(string tag)
        {
            Tags.Add(tag, true);
            return this;
        }

        public override string BuildValue(string k)
        {
            if (none)
            {
                return "";
            } else if (any)
            {
                return "!";
            } else if (Tags.TryGetValue(k,out bool b))
            {
                return b ? k : "!" + k;
            }
            return "error";
        }

        public static implicit operator TagSet(string tag)
        {
            return new TagSet().And(tag);
        }

        public TagSet Not(string tag)
        {
            Tags.Add(tag, false);
            return this;
        }

        
    }
}
