using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class CommandWrapper : Entities
    {

        public long Seed
        {
            get
            {
                FunctionWriter.Write("seed");
                return 0;
            }
        }
        private Gamemode gm;
        public Gamemode DefaultGamemode
        {
            get
            {
                FunctionWriter.Write("defaultgamemode");
                return gm ?? (gm = Gamemode.Survival);
            }
            set
            {
                gm = value;
                FunctionWriter.Write("defaultgamemode " + value);
            }
        }
        private Difficulty diff;
        public Difficulty Difficulty
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
        private Position wspawn;
        public Position WorldSpawn
        {
            get { return wspawn; }
            set
            {
                FunctionWriter.Write("setworldspawn " + value);
                wspawn = value;
            }
        }

        public Time Time
        {
            set
            {

            }
            get
            {
                return new Time();
            }
        }


        public override string ToString()
        {
            return "@s";
        }

        public void Clone(Position begin, Position end, Position destination)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination}");
        }

        public void Clone(Position begin, Position end, Position destination, MaskMode mask)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask}");
        }

        public void Clone(Position begin, Position end, Position destination, MaskMode mask, CloneMode mode)
        {
            FunctionWriter.Write($"clone {begin} {end} {destination} {mask} {mode}");
        }

        public void Fill(Position begin, Position end, Block block)
        {
            FunctionWriter.Write($"fill {begin} {end} {block}");
        }

        public void Fill(Position begin, Position end, Block block, FillMode mode)
        {
            FunctionWriter.Write($"fill {begin} {end} {block} {mode}");
        }

        public void Replace(Position begin, Position end, Block with, Block replace)
        {
            FunctionWriter.Write($"fill {begin} {end} {with} {FillMode.Replace.All(replace)}");
        }

        public void SetBlock(Position pos, Block block)
        {
            FunctionWriter.Write($"setblock {pos} {block}");
        }

        public void SetBlock(Position pos, Block block, SetMode mode)
        {
            FunctionWriter.Write($"setblock {pos} {block} {mode}");
        }

        public void Summon(EntityType type, Position pos)
        {
            FunctionWriter.Write($"summon {type} {pos}");
        }

        public void Summon(EntityType type, Position pos, NBT nbt)
        {
            FunctionWriter.Write($"summon {type} {pos} {nbt}");
        }

        public void SetWeather(Weather weather)
        {
            FunctionWriter.Write("weather " + weather);
        }

        public void SetWeather(Weather weather, uint duration)
        {
            if (duration > 1000000) throw new ArgumentException("weather duration must be less than 1,000,000");
            FunctionWriter.Write("weather " + weather + " " + duration);
        }

        public delegate void Execution(CommandWrapper wrapper);

        public void Do(Execution execute)
        {
            execute(this);
        }

        public void WriteRaw(string cmd)
        {
            FunctionWriter.Write(cmd);
        }

        private Namespace ns;
        public Namespace Namespace
        {
            get => ns;
            set
            {
                if (ns == null)
                {
                    ns = value;
                }
            }
        }


        public delegate void Function();

        public void RunFunction(Function func)
        {
            if (typeof(FunctionContainer).IsAssignableFrom(func.Target.GetType()))
            {
                FunctionContainer container = (FunctionContainer)func.Target;
                Type declarer = func.Method.DeclaringType;
                RunFunction(Namespace, declarer, func.Method.Name);
            }
        }

        public void RunFunction(string path)
        {
            if (path.Contains(":"))
            {
                FunctionWriter.Write("function " + path);
            }
            else
            {
                FunctionWriter.Write("function " + Namespace + ":" + path);
            }
        }

        public void RunFunction(Namespace ns, Type subFolder, string methodName)
        {
            string path = "";
            while (subFolder.DeclaringType != null)
            {
                subFolder = subFolder.DeclaringType;
                path = subFolder.Name.ToLower() + "/" + path;
            }
            path += methodName.ToLower();
            FunctionWriter.Write("function " + ns + ":" + path);
        }

    }
}
