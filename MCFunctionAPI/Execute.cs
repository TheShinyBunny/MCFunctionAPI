using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.Scoreboard;
using MCFunctionAPI.BossBar;

namespace MCFunctionAPI
{

    public class Execute
    {

        private string str = "";

        private CommandWrapper run;
        public CommandWrapper Run
        {
            get
            {
                FunctionWriter.Execute = this;
                return this.run;
            }
            private set
            {
                run = value;
            }
        }

        public Execute()
        {
            Run = new CommandWrapper();
            FunctionWriter.Execute = null;
        }
        
        public Execute As(EntitySelector entity)
        {
            return this + $"as {entity}";
        }

        private Execute Append(string s)
        {
            str += s + " ";
            return this;
        }

        public Execute At(EntitySelector entity)
        {
            return this + $"at {entity}";
        }

        public Execute At()
        {
            return this + "at @s";
        }

        public Execute Positioned(Position pos)
        {
            return this + $"positioned {pos}";
        }

        public Execute PositionedAs(EntitySelector entity)
        {
            return this + $"positioned as {entity}";
        }

        public Execute Align(Alignment a)
        {
            return this + $"align {a}";
        }
        
        public Execute Facing(Position pos)
        {
            return this + $"facing {pos}";
        }

        public Execute Facing(EntitySelector entity, Anchor anchor)
        {
            return this + $"facing entity {entity} {anchor}";
        }
        
        public Execute Rotated(Rotation rot)
        {
            return this + $"rotated {rot}";
        }

        public Execute RotatedAs(EntitySelector entity)
        {
            return this + $"rotated as {entity}";
        }

        public Execute Anchored(Anchor anchor)
        {
            return this + $"anchored {anchor}";
        }

        public Execute In(Dimension dim)
        {
            return this + $"in {dim}";
        }

        public Execute If(EntitySelector entity)
        {
            return this + $"if entity {entity}";
        }

        public Execute If(Position pos, Block block)
        {
            return this + $"if block {pos} {block}";
        }

        public Execute If(Position begin, Position end, Position dest, bool masked)
        {
            return this + $"if blocks {begin} {end} {dest} {(masked ? "masked" : "all")}";
        }

        public Execute If(string target, Objective targetObj, ScoreOperator @operator, string src, Objective srcObj)
        {
            return this + $"if score {target} {targetObj} {@operator} {src} {srcObj}";
        }

        public Execute If(string target, Objective targetObj, IntRange matches)
        {
            return this + $"if score {target} {targetObj} matches {matches}";
        }

        public Execute Unless(EntitySelector entity)
        {
            return this + $"unless entity {entity}";
        }

        public Execute Unless(Position pos, Block block)
        {
            return this + $"unless block {pos} {block}";
        }

        public Execute Unless(Position begin, Position end, Position dest, bool masked)
        {
            return this + $"unless blocks {begin} {end} {dest} {(masked ? "masked" : "all")}";
        }

        public Execute Unless(string target, Objective targetObj, ScoreOperator @operator, string src, Objective srcObj)
        {
            return this + $"unless score {target} {targetObj} {@operator} {src} {srcObj}";
        }

        public Execute Unless(string target, Objective targetObj, IntRange matches)
        {
            return this + $"unless score {target} {targetObj} matches {matches}";
        }

        public Execute Store(Storage @in, string name, Objective objective)
        {
            return this + $"store {@in} score {name} {objective}";
        }

        public Execute Store(Storage @in, BossBar.BossBar boss, bool max)
        {
            return this + $"store {@in} bossbar {boss} {(max ? "max" : "value")}";
        }

        public void Reset()
        {
            str = "";
        }

        public static Execute operator +(Execute e, string s)
        {
            return e.Append(s);
        }

        public override string ToString()
        {
            return "execute " + str;
        }
    }

    public class Storage
    {

        public static Storage Result = new Storage("result");
        public static Storage Success = new Storage("success");

        private string id;

        public Storage(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }
    }
    
}
