using MCFunctionAPI.Advancements;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class CommandWrapper
    {

        protected static Entities This = new Entities();
        

        protected static long Seed
        {
            get
            {
                FunctionWriter.Write("seed");
                return 0;
            }
        }
        private static Gamemode defgm;
        protected static Gamemode DefaultGamemode
        {
            get
            {
                FunctionWriter.Write("defaultgamemode");
                return defgm ?? (defgm = Gamemode.Survival);
            }
            set
            {
                defgm = value;
                FunctionWriter.Write("defaultgamemode " + value);
            }
        }
        private static Difficulty diff;
        protected static Difficulty Difficulty
        {
            get
            {
                FunctionWriter.Write("difficulty");
                return diff ?? (diff = Difficulty.Normal);
            }
            set
            {
                diff = value;
                FunctionWriter.Write("difficulty " + value);
            }
        }
        private static Position wspawn;
        protected static Position WorldSpawn
        {
            get { return wspawn; }
            set
            {
                FunctionWriter.Write("setworldspawn " + value);
                wspawn = value;
            }
        }

        protected static Time Time
        {
            set
            {

            }
            get
            {
                return new Time();
            }
        }

        private static TagList tags;
        protected static TagList Tags { get => tags ?? (tags = new TagList(This)); private set { tags = value; } }
        private static XP xp;
        protected static XP XP { get => xp ?? (xp = new XP(This)); private set { xp = value; } }
        private static AdvancementList adv;
        protected static AdvancementList Advancements { get => adv ?? (adv = new AdvancementList(This)); private set { adv = value; } }
        private static Position spoint;
        protected static Position SpawnPoint
        {
            get { return spoint; }
            set
            {
                FunctionWriter.Write("spawnpoint " + value);
                spoint = value;
            }
        }
        private static Gamemode gm;
        protected static Gamemode Gamemode
        {
            get
            {
                return gm ?? (gm = Gamemode.Survival);
            }
            set
            {
                gm = value;
                FunctionWriter.Write("gamemode " + value + " " + This);
            }
        }

        protected static void GiveEffect(Effect effect)
        {
            This.GiveEffect(effect);
        }

        protected static void GiveEffect(Effect effect, uint seconds)
        {
            This.GiveEffect(effect, seconds);
        }

        protected static void GiveEffect(Effect effect, uint seconds, uint level)
        {
            This.GiveEffect(effect, seconds, level);
        }

        protected static void GiveEffect(Effect effect, uint seconds, uint level, bool particles)
        {
            This.GiveEffect(effect, seconds, level, particles);
        }

        protected static void ClearEffects()
        {
            This.ClearEffects();
        }

        protected static void ClearEffect(Effect effect)
        {
            This.ClearEffect(effect);
        }

        protected static void Kill()
        {
            This.Kill();
        }

        protected static void ReplaceItem(string slot, Item item)
        {
            This.ReplaceItem(slot, item);
        }

        protected static void ReplaceItem(string slot, Item item, uint count)
        {
            This.ReplaceItem(slot, item, count);
        }

        protected static void Say(string msg)
        {
            FunctionWriter.Write("say " + msg);
        }

        protected static void LeaveTeam()
        {
            This.LeaveTeam();
        }

        protected static void JoinTeam(Team team)
        {
            This.JoinTeam(team);
        }

        protected static void Spread(double x, double z, float distance, float maxRange, bool respectTeams)
        {
            This.Spread(x, z, distance, maxRange, respectTeams);
        }

        protected static void Teleport(Position destination)
        {
            FunctionWriter.Write("tp " + destination);
        }

        protected static void Teleport(EntitySelector entity)
        {
            This.Teleport(entity);
        }

        protected static void TeleportFacing(Position pos, Position facing)
        {
            This.TeleportFacing(pos,facing);
        }

        protected static void TeleportFacing(Position pos, EntitySelector facing)
        {
            This.TeleportFacing(pos,facing);
        }

        protected static void TeleportFacing(Position pos, EntitySelector facing, Anchor anchor)
        {
            This.TeleportFacing(pos, facing, anchor);
        }

        protected static void Teleport(Position pos, Rotation rotation)
        {
            This.Teleport(pos, rotation);
        }

        protected static void Trigger(Objective o)
        {
            FunctionWriter.Write($"trigger {o}");
        }

        protected static void TriggerAdd(Objective o, int add)
        {
            FunctionWriter.Write($"trigger {o} add {add}");
        }

        protected static void TriggerSet(Objective o, int set)
        {
            FunctionWriter.Write($"trigger {o} set {set}");
        }


        protected static ResultCommand GetData(string path)
        {
            return This.GetData(path);
        }

        protected static ResultCommand GetData(string path, double scale)
        {
            return This.GetData(path, scale);
        }

        protected static void MergeData(NBT nbt)
        {
            This.MergeData(nbt);
        }

        protected static void RemoveData(string path)
        {
            This.RemoveData(path);
        }

        protected static void ClearInventory()
        {
            This.ClearInventory();
        }

        protected static void Clear(Item item)
        {
            This.Clear(item);
        }

        protected static void Clear(Item item, int count)
        {
            This.Clear(item, count);
        }

        protected static void Enchant(Enchantment ench)
        {
            This.Enchant(ench);
        }

        protected static void Enchant(Enchantment ench, int level)
        {
            This.Enchant(ench, level);
        }

        protected static void Give(Item item)
        {
            This.Give(item);
        }

        protected static void Give(Item item, int count)
        {
            This.Give(item, count);
        }

        protected static void PlaySound(ResourceLocation sound, SoundSource src)
        {
            This.PlaySound(sound, src);
        }

        protected static void PlaySound(ResourceLocation sound, SoundSource src, Position pos)
        {
            This.PlaySound(sound, src, pos);
        }

        protected static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume)
        {
            This.PlaySound(sound, src, pos, volume);
        }

        protected static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch)
        {
            This.PlaySound(sound, src, pos, volume, pitch);
        }

        protected static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch, float minVolume)
        {
            This.PlaySound(sound, src, pos, volume, pitch, minVolume);
        }

        protected static void StopSound()
        {
            This.StopSound();
        }

        protected static void StopSound(SoundSource src)
        {
            This.StopSound(src);
        }

        protected static void StopSound(SoundSource src, ResourceLocation sound)
        {
            This.StopSound(src, sound);
        }

        protected static void TellRaw(TextComponent text)
        {
            This.TellRaw(text);
        }

        protected static void Title(TextComponent title)
        {
            This.Title(title);
        }

        protected static void Subtitle(TextComponent subtitle)
        {
            This.Subtitle(subtitle);
        }

        protected static void ClearTitle()
        {
            This.ClearTitle();
        }

        protected static void ResetTitle()
        {
            This.ResetTitle();
        }

        protected static void SetTitleTimes(int fadeIn, int stay, int fadeOut)
        {
            This.SetTitleTimes(fadeIn, stay, fadeOut);
        }

        protected static void Actionbar(TextComponent actionbar)
        {
            This.Actionbar(actionbar);
        }

        protected static void Clone(Position begin, Position end, Position destination)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination}");
        }

        protected static void Clone(Position begin, Position end, Position destination, MaskMode mask)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask}");
        }

        protected static void Clone(Position begin, Position end, Position destination, MaskMode mask, CloneMode mode)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask} {mode}");
        }

        protected static void Fill(Position begin, Position end, Block block)
        {
            FunctionWriter.Write($"fill {begin} {end} {block}");
        }

        protected static void Fill(Position begin, Position end, Block block, FillMode mode)
        {
            FunctionWriter.Write($"fill {begin} {end} {block} {mode}");
        }

        protected static void Replace(Position begin, Position end, Block with, Block replace)
        {
            FunctionWriter.Write($"fill {begin} {end} {with} {FillMode.Replace.All(replace)}");
        }

        protected static void SetBlock(Position pos, Block block)
        {
            FunctionWriter.Write($"setblock {pos} {block}");
        }

        protected static void SetBlock(Position pos, Block block, SetMode mode)
        {
            FunctionWriter.Write($"setblock {pos} {block} {mode}");
        }

        protected static void Summon(EntityType type, Position pos)
        {
            FunctionWriter.Write($"summon {type} {pos}");
        }

        protected static void Summon(EntityType type, Position pos, NBT nbt)
        {
            FunctionWriter.Write($"summon {type} {pos} {nbt}");
        }

        protected static void SetWeather(Weather weather)
        {
            FunctionWriter.Write("weather " + weather);
        }

        protected static void Schedule(Function func, int ticks)
        {
            FunctionWriter.Write("schedule function " + FunctionWriter.GetFunctionPath(FunctionWriter.Namespace,func) + " " + ticks);
        }

        protected static void SetWeather(Weather weather, uint duration)
        {
            if (duration > 1000000) throw new ArgumentException("weather duration must be less than 1,000,000");
            FunctionWriter.Write("weather " + weather + " " + duration);
        }
        

        /// <summary>
        /// Runs the commands specified in the Execution delegate.
        /// </summary>
        /// <param name="execute"></param>
        protected static void Do(Action execute)
        {
            execute();
        }

        /// <summary>
        /// Writes the specified <paramref name="cmd"/> straight to the FunctionWriter.
        /// </summary>
        /// <param name="cmd">The raw command to write</param>
        protected static void WriteRaw(string cmd)
        {
            FunctionWriter.Write(cmd);
        }

        public delegate void Function();

        public delegate void ParameterFunction(string param);

        /// <summary>
        /// Runs the specified function method.
        /// </summary>
        /// <param name="func"></param>
        public static void RunFunction(Function func)
        {
            FunctionWriter.Write("function " + FunctionWriter.GetFunctionPath(FunctionWriter.Namespace,func));
        }

        public static void RunAbstractFunction(object instance,Function func)
        {
            FunctionWriter.Write("function " + FunctionWriter.GetFunctionPath(FunctionWriter.Namespace,(Function)Delegate.CreateDelegate(typeof(Function), instance, func.Method.Name),instance.GetType()));
        }

        /// <summary>
        /// Prints a space line in the function output
        /// </summary>
        protected static void Space()
        {
            FunctionWriter.Space();
        }

        /// <summary>
        /// Runs the function of the specified path.
        /// </summary>
        /// <param name="path"></param>
        public static void RunFunction(string path)
        {
            if (path.Contains(":"))
            {
                FunctionWriter.Write("function " + path);
            }
            else
            {
                FunctionWriter.Write("function " + FunctionWriter.Namespace + ":" + path);
            }
        }
        

    }
}
