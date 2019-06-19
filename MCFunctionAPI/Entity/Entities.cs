using MCFunctionAPI.Advancements;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class Entities : DataContainer, IInventoryHolder
    {
        private TagList tags;
        public TagList Tags { get => tags ?? (tags = new TagList(this)); private set { tags = value; } }
        private XP xp;
        public XP XP { get => xp ?? (xp = new XP(this)); private set { xp = value; } }
        private AdvancementList adv;
        public AdvancementList Advancements { get => adv ?? (adv = new AdvancementList(this)); private set { adv = value; } }
        private Position spoint;
        public Position SpawnPoint
        {
            get { return spoint; }
            set
            {
                FunctionWriter.Write("spawnpoint " + value + " " + this);
                spoint = value;
            }
        }
        private Gamemode gm;
        public Gamemode Gamemode
        {
            get
            {
                return gm ?? (gm = Gamemode.Survival);
            }
            set
            {
                gm = value;
                FunctionWriter.Write("gamemode " + value + " " + this);
            }
        }

        public void GiveEffect(Effect effect)
        {
            FunctionWriter.Write($"effect give {this} {effect}");
        }

        public void GiveEffect(Effect effect, uint seconds)
        {
            if (seconds > 1000000)
            {
                throw new ArgumentException("Effect duration must be smaller than 1,000,000!");
            }
            FunctionWriter.Write($"effect give {this} {effect} {seconds}");
        }

        public void GiveEffect(Effect effect, uint seconds, uint level)
        {
            if (seconds > 1000000)
            {
                throw new ArgumentException("Effect duration must be smaller than 1,000,000!");
            }
            if (level > 255)
            {
                throw new ArgumentException("Effect amplifier must be smaller than 256!");
            }
            FunctionWriter.Write($"effect give {this} {effect} {seconds} {level}");
        }

        public void GiveEffect(Effect effect, uint seconds, uint level, bool particles)
        {
            if (seconds > 1000000)
            {
                throw new ArgumentException("Effect duration must be smaller than 1,000,000!");
            }
            if (level > 255)
            {
                throw new ArgumentException("Effect amplifier must be smaller than 256!");
            }
            FunctionWriter.Write($"effect give {this} {effect} {seconds} {level} {(!particles).ToString().ToLower()}");
        }

        public void ClearEffects()
        {
            FunctionWriter.Write("effect clear " + this);
        }

        public void ClearEffect(Effect effect)
        {
            FunctionWriter.Write("effect clear " + this + " " + effect);
        }

        public void Kill()
        {
            FunctionWriter.Write("kill " + this);
        }

        public void ReplaceItem(string slot, Item item)
        {
            FunctionWriter.Write($"replaceitem entity {this} {slot} {item}");
        }

        public void ReplaceItem(string slot, Item item, uint count)
        {
            FunctionWriter.Write($"replaceitem entity {this} {slot} {item} {count}");
        }

        public void Say(string msg)
        {
            FunctionWriter.Write("say " + this + " " + msg);
        }

        public void LeaveTeam()
        {
            FunctionWriter.Write("team leave " + this);
        }

        public void JoinTeam(Team team)
        {
            team.Add(this.ToString());
        }

        public void Spread(double x, double z, float distance, float maxRange, bool respectTeams)
        {
            FunctionWriter.Write("spreadplayers " + x + " " + z + " " + distance + " " + maxRange + " " + respectTeams + " " + this);
        }

        public void Teleport(Position destination)
        {
            FunctionWriter.Write("tp " + this + " " + destination);
        }

        public void Teleport(EntitySelector entity)
        {
            FunctionWriter.Write("tp " + this + " " + entity);
        }

        public void TeleportFacing(Position pos, Position facing)
        {
            FunctionWriter.Write("tp " + this + " " + pos + " facing " + facing);
        }

        public  void TeleportFacing(Position pos, EntitySelector facing)
        {
            FunctionWriter.Write("tp " + this + " " + pos + " facing entity " + facing);
        }

        public void TeleportFacing(Position pos, EntitySelector facing, Anchor anchor)
        {
            FunctionWriter.Write("tp " + this + " " + pos + " facing entity " + facing + " " + anchor);
        }

        public void Teleport(Position pos, Rotation rotation)
        {
            FunctionWriter.Write("tp " + this + " " + pos + " " + rotation);
        }

        public void Trigger(Objective o)
        {
            FunctionWriter.Write($"execute as {this} trigger {o}");
        }

        public void TriggerAdd(Objective o, int add)
        {
            FunctionWriter.Write($"execute as {this} trigger {o} add {add}");
        }

        public void TriggerSet(Objective o, int set)
        {
            FunctionWriter.Write($"execute as {this} trigger {o} set {set}");
        }


        public override ResultCommand GetData(string path)
        {
            return new ResultCommand($"data get entity {this} {path}");
        }

        public override ResultCommand GetData(string path, double scale)
        {
            return new ResultCommand($"data get entity {this} {path} {scale}");
        }

        public override void MergeData(NBT nbt)
        {
            FunctionWriter.Write($"data merge entity {this} {nbt}");
        }

        public override void RemoveData(string path)
        {
            FunctionWriter.Write($"data remove entity {this} {path}");
        }
        
        public void ClearInventory()
        {
            FunctionWriter.Write("clear " + this);
        }

        public void Clear(Item item)
        {
            FunctionWriter.Write($"clear {this} {item}");
        }

        public void Clear(Item item, int count)
        {
            FunctionWriter.Write($"clear {this} {item} {count}");
        }

        public void Enchant(Enchantment ench)
        {
            FunctionWriter.Write($"enchant {this} {ench}");
        }

        public void Enchant(Enchantment ench, int level)
        {
            if (level > ench.MaxLevel)
            {
                throw new ArgumentException("Enchantment " + ench + " has a maximum level of " + ench.MaxLevel + "!");
            }
            FunctionWriter.Write($"enchant {this} {ench} {level}");
        }

        public void Give(Item item)
        {
            FunctionWriter.Write("give " + this + " " + item);
        }

        public void Give(Item item, int count)
        {
            FunctionWriter.Write("give " + this + " " + item + " " + count);
        }

        public void PlaySound(ResourceLocation sound, SoundSource src)
        {
            FunctionWriter.Write("playsound " + sound + " " + src);
        }

        public void PlaySound(ResourceLocation sound, SoundSource src, Position pos)
        {
            FunctionWriter.Write("playsound " + sound + " " + src + " " + pos);
        }

        public void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume)
        {
            FunctionWriter.Write("playsound " + sound + " " + src + " " + pos + " " + volume);
        }

        public void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch)
        {
            FunctionWriter.Write("playsound " + sound + " " + src + " " + pos + " " + volume + " " + pitch);
        }

        public void PlaySound(ResourceLocation sound, SoundSource src, Position pos, float volume, float pitch, float minVolume)
        {
            FunctionWriter.Write("playsound " + sound + " " + src + " " + pos + " " + volume + " " + pitch + " " + minVolume);
        }

        public void StopSound()
        {
            FunctionWriter.Write("stopsound " + this);
        }

        public void StopSound(SoundSource src)
        {
            FunctionWriter.Write($"stopsound {this} {src}");
        }

        public void StopSound(SoundSource src, ResourceLocation sound)
        {
            FunctionWriter.Write($"stopsound {this} {src} {sound}");
        }

        public void TellRaw(TextComponent text)
        {
            FunctionWriter.Write("tellraw " + this + " " + text);
        }

        public void Title(TextComponent title)
        {
            FunctionWriter.Write("title " + this + " title " + title);
        }

        public void Subtitle(TextComponent subtitle)
        {
            FunctionWriter.Write("title " + this + " subtitle " + subtitle);
        }

        public void ClearTitle()
        {
            FunctionWriter.Write("title " + this + " clear");
        }

        public void ResetTitle()
        {
            FunctionWriter.Write("title " + this + " reset");
        }

        public void SetTitleTimes(int fadeIn, int stay, int fadeOut)
        {
            FunctionWriter.Write("title " + this + " times " + fadeIn + " " + stay + " " + fadeOut);
        }

        public void Actionbar(TextComponent actionbar)
        {
            FunctionWriter.Write("title " + this + " actionbar " + actionbar);
        }
        
        public static implicit operator string(Entities entities)
        {
            return entities.ToString();
        }

        public override string ToString()
        {
            return "@s";
        }

        public override string ToDataCommand()
        {
            return "entity " + this;
        }
    }
}
