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

        public static Entities This = new Entities();
        

        public static long Seed
        {
            get
            {
                FunctionWriter.Write("seed");
                return 0;
            }
        }
        private static Gamemode defgm;
        public static Gamemode DefaultGamemode
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
        public static Difficulty Difficulty
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
        public static Position WorldSpawn
        {
            get { return wspawn; }
            set
            {
                FunctionWriter.Write("setworldspawn " + value);
                wspawn = value;
            }
        }

        public static Time Time
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
        public static TagList Tags { get => tags ?? (tags = new TagList(This)); private set { tags = value; } }
        private static XP xp;
        public static XP XP { get => xp ?? (xp = new XP(This)); private set { xp = value; } }
        private static AdvancementList adv;
        public static AdvancementList Advancements { get => adv ?? (adv = new AdvancementList(This)); private set { adv = value; } }
        private static Position spoint;
        public static Position SpawnPoint
        {
            get { return spoint; }
            set
            {
                FunctionWriter.Write("spawnpoint " + value);
                spoint = value;
            }
        }
        private static Gamemode gm;
        public static Gamemode Gamemode
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

        public static void GiveEffect(Effect effect)
        {
            This.GiveEffect(effect);
        }

        public static void GiveEffect(Effect effect, uint seconds)
        {
            This.GiveEffect(effect, seconds);
        }

        public static void GiveEffect(Effect effect, uint seconds, uint level)
        {
            This.GiveEffect(effect, seconds, level);
        }

        public static void GiveEffect(Effect effect, uint seconds, uint level, bool particles)
        {
            This.GiveEffect(effect, seconds, level, particles);
        }

        public static void ClearEffects()
        {
            This.ClearEffects();
        }

        public static void ClearEffect(Effect effect)
        {
            This.ClearEffect(effect);
        }

        public static void Kill()
        {
            This.Kill();
        }

        public static void ReplaceItem(string slot, Item item)
        {
            This.ReplaceItem(slot, item);
        }

        public static void ReplaceItem(string slot, Item item, uint count)
        {
            This.ReplaceItem(slot, item, count);
        }

        public static void Say(string msg)
        {
            FunctionWriter.Write("say " + msg);
        }

        public static void LeaveTeam()
        {
            This.LeaveTeam();
        }

        public static void JoinTeam(Team team)
        {
            This.JoinTeam(team);
        }

        public static void Spread(double x, double z, float distance, float maxRange, bool respectTeams)
        {
            This.Spread(x, z, distance, maxRange, respectTeams);
        }

        public static void Teleport(Position destination)
        {
            FunctionWriter.Write("tp " + destination);
        }

        public static void Teleport(EntitySelector entity)
        {
            This.Teleport(entity);
        }

        public static void TeleportFacing(Position pos, Position facing)
        {
            This.TeleportFacing(pos,facing);
        }

        public static void TeleportFacing(Position pos, EntitySelector facing)
        {
            This.TeleportFacing(pos,facing);
        }

        public static void TeleportFacing(Position pos, EntitySelector facing, Anchor anchor)
        {
            This.TeleportFacing(pos, facing, anchor);
        }

        public static void Teleport(Position pos, Rotation rotation)
        {
            This.Teleport(pos, rotation);
        }

        public static void Trigger(Objective o)
        {
            FunctionWriter.Write($"trigger {o}");
        }

        public static void TriggerAdd(Objective o, int add)
        {
            FunctionWriter.Write($"trigger {o} add {add}");
        }

        public static void TriggerSet(Objective o, int set)
        {
            FunctionWriter.Write($"trigger {o} set {set}");
        }


        public static ResultCommand GetData(string path)
        {
            return This.GetData(path);
        }

        public static ResultCommand GetData(string path, double scale)
        {
            return This.GetData(path, scale);
        }

        public static void MergeData(NBT nbt)
        {
            This.MergeData(nbt);
        }

        public static void RemoveData(string path)
        {
            This.RemoveData(path);
        }

        public static void ClearInventory()
        {
            This.ClearInventory();
        }

        public static void Clear(Item item)
        {
            This.Clear(item);
        }

        public static void Clear(Item item, int count)
        {
            This.Clear(item, count);
        }

        public static void Enchant(Enchantment ench)
        {
            This.Enchant(ench);
        }

        public static void Enchant(Enchantment ench, int level)
        {
            This.Enchant(ench, level);
        }

        public static void Give(Item item)
        {
            This.Give(item);
        }

        public static void Give(Item item, int count)
        {
            This.Give(item, count);
        }

        public static void PlaySound(ResourceLocation sound, SoundSource src)
        {
            This.PlaySound(sound, src);
        }

        public static void PlaySound(ResourceLocation sound, SoundSource src, Position pos)
        {
            This.PlaySound(sound, src, pos);
        }

        public static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume)
        {
            This.PlaySound(sound, src, pos, volume);
        }

        public static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch)
        {
            This.PlaySound(sound, src, pos, volume, pitch);
        }

        public static void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch, float minVolume)
        {
            This.PlaySound(sound, src, pos, volume, pitch, minVolume);
        }

        public static void StopSound()
        {
            This.StopSound();
        }

        public static void StopSound(SoundSource src)
        {
            This.StopSound(src);
        }

        public static void StopSound(SoundSource src, ResourceLocation sound)
        {
            This.StopSound(src, sound);
        }

        public static void TellRaw(TextComponent text)
        {
            This.TellRaw(text);
        }

        public static void Title(TextComponent title)
        {
            This.Title(title);
        }

        public static void Subtitle(TextComponent subtitle)
        {
            This.Subtitle(subtitle);
        }

        public static void ClearTitle()
        {
            This.ClearTitle();
        }

        public static void ResetTitle()
        {
            This.ResetTitle();
        }

        public static void SetTitleTimes(int fadeIn, int stay, int fadeOut)
        {
            This.SetTitleTimes(fadeIn, stay, fadeOut);
        }

        public static void Actionbar(TextComponent actionbar)
        {
            This.Actionbar(actionbar);
        }

        public static void Clone(Position begin, Position end, Position destination)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination}");
        }

        public static void Clone(Position begin, Position end, Position destination, MaskMode mask)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask}");
        }

        public static void Clone(Position begin, Position end, Position destination, MaskMode mask, CloneMode mode)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask} {mode}");
        }

        public static void Fill(Position begin, Position end, Block block)
        {
            FunctionWriter.Write($"fill {begin} {end} {block}");
        }

        public static void Fill(Position begin, Position end, Block block, FillMode mode)
        {
            FunctionWriter.Write($"fill {begin} {end} {block} {mode}");
        }

        public static void Replace(Position begin, Position end, Block with, Block replace)
        {
            FunctionWriter.Write($"fill {begin} {end} {with} {FillMode.Replace.All(replace)}");
        }

        public static void SetBlock(Position pos, Block block)
        {
            FunctionWriter.Write($"setblock {pos} {block}");
        }

        public static void SetBlock(Position pos, Block block, SetMode mode)
        {
            FunctionWriter.Write($"setblock {pos} {block} {mode}");
        }

        public static void Summon(EntityType type, Position pos)
        {
            FunctionWriter.Write($"summon {type} {pos}");
        }

        public static void Summon(EntityType type, Position pos, NBT nbt)
        {
            FunctionWriter.Write($"summon {type} {pos} {nbt}");
        }

        public static void SetWeather(Weather weather)
        {
            FunctionWriter.Write("weather " + weather);
        }

        public static void Schedule(Function func, int ticks)
        {
            FunctionWriter.Write("schedule function " + FunctionWriter.GetFunctionPath(func) + " " + ticks);
        }

        public static void SetWeather(Weather weather, uint duration)
        {
            if (duration > 1000000) throw new ArgumentException("weather duration must be less than 1,000,000");
            FunctionWriter.Write("weather " + weather + " " + duration);
        }
        

        /// <summary>
        /// Runs the commands specified in the Execution delegate.
        /// </summary>
        /// <param name="execute"></param>
        public static void Do(Action execute)
        {
            execute();
        }

        /// <summary>
        /// Writes the specified <paramref name="cmd"/> straight to the FunctionWriter.
        /// </summary>
        /// <param name="cmd">The raw command to write</param>
        public static void WriteRaw(string cmd)
        {
            FunctionWriter.Write(cmd);
        }


        public delegate void Function();

        /// <summary>
        /// Runs the specified function method.
        /// </summary>
        /// <param name="func"></param>
        public static void RunFunction(Function func)
        {
            FunctionWriter.Write("function " + FunctionWriter.GetFunctionPath(func));
        }

        public static void RunAbstractFunction(object instance,Function func)
        {
            FunctionWriter.Write("function " + FunctionWriter.GetFunctionPath((Function)Delegate.CreateDelegate(typeof(Function), instance, func.Method.Name),instance.GetType()));
        }

        /// <summary>
        /// Prints a space line in the function output
        /// </summary>
        public static void Space()
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
