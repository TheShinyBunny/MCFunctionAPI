using MCFunctionAPI.Advancements;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MCFunctionAPI.CommandWrapper;

namespace MCFunctionAPI.Entity
{
    public class EntitySelector : Entities
    {

        private Target target;
        public double? X;
        public double? Y;
        public double? Z;
        public IntRange Distance;
        public double? DX;
        public double? DY;
        public double? DZ;
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
        public NBT NBT;
        public bool NegateNBT;

        public Execute Execute { get; }

        private static readonly Regex Splitter = new Regex("(\\w+)={(.*,?.*)}");

        public static EntitySelector Self { get => new EntitySelector("@s"); }
        public static EntitySelector AllEntities { get => new EntitySelector("@e"); }
        public static EntitySelector AllPlayers { get => new EntitySelector("@a"); }
        public static EntitySelector RandomPlayer { get => new EntitySelector("@r"); }
        public static EntitySelector ClosestPlayer { get => new EntitySelector("@p"); }
        

        public EntitySelector(Target target)
        {
            this.target = target;
            Scores = new ScoreSet();
            Tags = new TagSet();
            Team = new TeamArgument();
            Name = new NameArgument();
            Type = new TypeArgument();
            Execute = new Execute(this);
            Gamemode = new GamemodeArgument();
            Advancements = new AdvancementsArgument();
        }

        public EntitySelector Tag(string tag)
        {
            Tags.Has(tag);
            return this;
        }

        public EntitySelector Coords(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            return this;
        }

        public EntitySelector Volume(double x, double y, double z)
        {
            DX = x;
            DY = y;
            DZ = z;
            return this;
        }

        public EntitySelector InDistance(IntRange range)
        {
            Distance = range;
            return this;
        }

        public EntitySelector HasLevel(IntRange range)
        {
            Level = range;
            return this;
        }

        public EntitySelector Rotation(IntRange x, IntRange y)
        {
            X_Rotation = x;
            Y_Rotation = y;
            return this;
        }

        public EntitySelector Score(Objective obj, IntRange range)
        {
            Scores.Where(obj, range);
            return this;
        }

        public EntitySelector Score(ScoreRange score)
        {
            Scores.Where(score.obj, score.range);
            return this;
        }

        public EntitySelector InTeam(Team team)
        {
            Team.Is(team);
            return this;
        }

        public EntitySelector NotInTeam(Team team)
        {
            Team.IsNot(team);
            return this;
        }

        public EntitySelector InAnyTeam()
        {
            Team.Any();
            return this;
        }

        public EntitySelector InNoTeam()
        {
            Team.None();
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

        public EntitySelector LimitTo(uint limit)
        {
            Limit = limit;
            return this;
        }

        public EntitySelector InGamemode(Gamemode gm)
        {
            Gamemode.Is(gm);
            return this;
        }


        public EntitySelector NotInGamemode(Gamemode gm)
        {
            Gamemode.IsNot(gm);
            return this;
        }

        public EntitySelector NameIs(string name)
        {
            Name.Is(name);
            return this;
        }

        public EntitySelector NameIsNot(string name)
        {
            Name.IsNot(name);
            return this;
        }

        public EntitySelector Is(EntityType type)
        {
            Type.Is(type);
            return this;
        }

        public EntitySelector IsNot(EntityType type)
        {
            Type.Not(type);
            return this;
        }

        public EntitySelector SortBy(Sort sort)
        {
            Sort = sort;
            return this;
        }

        public EntitySelector HasNBT(NBT nbt)
        {
            NBT = nbt;
            return this;
        }

        public EntitySelector IsSleeping()
        {
            if (NBT == null)
            {
                NBT = new NBT();
            }
            NBT.Set("SleepTimer", 0);
            NegateNBT = true;
            return this;
        }

        public EntitySelector NotSleeping()
        {
            if (NBT == null)
            {
                NBT = new NBT();
            }
            NBT.Set("SleepTimer", 0);
            return this;
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

        /// <summary>
        /// Runs the specified function method.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="at">Whether or not use the "at" execute</param>
        public void RunFunction(bool at, Function func)
        {
            if (at)
            {
                Execute.At().RunFunction(func);
            } else
            {
                RunFunction(func);
            }
        }

        /// <summary>
        /// Runs the specified function method, only as the entity(s), and not at.
        /// </summary>
        /// <param name="func"></param>
        public void RunFunction(Function func)
        {
            Execute.RunFunction(func);
        }


        public override string ToString()
        {
            List<string> args = new List<string>();
            if (X != null)
            {
                args.Add("x=" + X);
                args.Add("y=" + Y);
                args.Add("z=" + Y);
            }
            
            AddArg(args, nameof(Distance), Distance);
            if (DX != null)
            {
                args.Add("dx=" + DX);
                args.Add("dy=" + DY);
                args.Add("dz=" + DZ);
            }
            AddArg(args, nameof(Level), Level);
            AddArg(args, nameof(X_Rotation), X_Rotation);
            AddArg(args, nameof(Y_Rotation), Y_Rotation);
            AddArg(args, nameof(Scores), Scores);
            AddArg(args, "tag", Tags);
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
            if (NBT != null)
            {
                if (NegateNBT)
                {
                    args.Add("nbt=!" + NBT.ToString());
                }
                else
                {
                    args.Add("nbt=" + NBT.ToString());
                }
            }

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


        public static implicit operator string(EntitySelector selector)
        {
            return selector.ToString();
        }

        public static implicit operator EntitySelector(string s)
        {
            int sqbrace = s.IndexOf('[');
            if (sqbrace == -1)
            {
                return new EntitySelector(s);
            }
            EntitySelector selector = new EntitySelector(s.Substring(0, sqbrace));
            string args = s.Substring(sqbrace + 1, s.LastIndexOf(']') - 3);
            Match match = Splitter.Match(args);
            while (match.Success)
            {
                string value = match.Groups[2].Value;
                switch (match.Groups[1].Value)
                {
                    case "scores":
                        selector.Scores = value;
                        break;
                    case "nbt":
                        selector.NBT = value;
                        break;
                    default:
                        break;
                }
                match = match.NextMatch();
            }
            args = string.Join("",Splitter.Split(args));
            foreach (var arg in args.Split(','))
            {
                if (arg != "")
                {
                    string key = arg.Split('=')[0];
                    string value = arg.Split('=')[1];
                    switch (key)
                    {
                        case "x":
                            selector.X = double.Parse(value);
                            break;
                        case "y":
                            selector.Y = double.Parse(value);
                            break;
                        case "z":
                            selector.Z = double.Parse(value);
                            break;
                        case "distance":
                            selector.Distance = value;
                            break;
                        case "dx":
                            selector.DX = double.Parse(value);
                            break;
                        case "dy":
                            selector.DY = double.Parse(value);
                            break;
                        case "dz":
                            selector.DZ = double.Parse(value);
                            break;
                        case "level":
                            selector.Level = value;
                            break;
                        case "x_rotation":
                            selector.X_Rotation = value;
                            break;
                        case "y_rotation":
                            selector.Y_Rotation = value;
                            break;
                        case "tag":
                            switch (value)
                            {
                                case "":
                                    selector.HasNoTags();
                                    break;
                                case "!":
                                    selector.HasTags();
                                    break;
                                default:
                                    if (value.StartsWith("!"))
                                    {
                                        selector.NotTag(value.Substring(1));
                                    }
                                    else
                                    {
                                        selector.Tag(value);
                                    }
                                    break;
                            }
                            break;
                        case "team":
                            switch (value)
                            {
                                case "":
                                    selector.InNoTeam();
                                    break;
                                case "!":
                                    selector.InAnyTeam();
                                    break;
                                default:
                                    if (value.StartsWith("!"))
                                    {
                                        selector.NotInTeam(value.Substring(1));
                                    }
                                    else
                                    {
                                        selector.InTeam(value);
                                    }
                                    break;
                            }
                            break;
                        case "limit":
                            selector.LimitTo(uint.Parse(value));
                            break;
                        case "gamemode":
                            selector.InGamemode(value);
                            break;
                        case "name":
                            if (value.StartsWith("!"))
                            {
                                selector.NameIsNot(value.Substring(1));
                            } else
                            {
                                selector.NameIs(value);
                            }
                            break;
                        case "type":
                            if (value.StartsWith("!"))
                            {
                                selector.Type.Not(value.Substring(1));
                            }
                            else
                            {
                                selector.Type.Is(value);
                            }
                            break;
                        case "sort":
                            selector.SortBy(value);
                            break;
                        default:
                            break;
                    }
                }
            }
            return selector;
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

        public AdvancementValue this[Advancement advancement]
        {
            set
            {
                Advancements.Add(advancement, value);
            }
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

        public class AdvancementValue
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
            not = name.StartsWith("!");
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
            return $"{(not ? "!" : "")}\"{name}\"";
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

    public class ScoreSet : SingleArgument, INBTSerializable
    {
        public IDictionary<Objective, IntRange> Scores { get; }

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

        public IntRange this[Objective obj]
        {
            set
            {
                Scores.Add(obj, value);
            }
        }

        public override bool ShouldAdd()
        {
            return Scores.Count != 0;
        }

        public object ToNBT()
        {
            NBT nbt = new NBT();
            foreach (var e in Scores)
            {
                nbt.Set(e.Key.Name, e.Value);
            }
            return nbt;
        }

        public static implicit operator ScoreSet(string s)
        {
            string[] pairs = s.Split(',');
            ScoreSet set = new ScoreSet();
            foreach (var p in pairs)
            {
                string o = p.Split('=')[0];
                string v = p.Split('=')[1];
                set.Where(o, v);
            }
            return set;
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

        public TagSet Has(string tag)
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
            return new TagSet().Has(tag);
        }

        public TagSet Not(string tag)
        {
            Tags.Add(tag, false);
            return this;
        }

        
    }
}
