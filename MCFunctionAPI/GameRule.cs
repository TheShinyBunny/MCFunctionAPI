using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    public class GameRule : EnumBase
    {
        public static BooleanRule AnnounceAdvancements = new BooleanRule("announceAdvancements");
        public static BooleanRule CommandBlockOutput = new BooleanRule("commandBlockOutput");
        public static BooleanRule DisableElytraMovementCheck = new BooleanRule("disableElytraMovementCheck");
        public static BooleanRule DoDaylightCycle = new BooleanRule("doDaylightCycle");
        public static BooleanRule DoEntityDrops = new BooleanRule("doEntityDrops");
        public static BooleanRule DoFireTick = new BooleanRule("doFireTick");
        public static BooleanRule DoLimitedCrafting = new BooleanRule("doLimitedCrafting");
        public static BooleanRule DoMobLoot = new BooleanRule("doMobLoot");
        public static BooleanRule DoMobSpawning = new BooleanRule("doMobSpawning");
        public static BooleanRule DoTileDrops = new BooleanRule("doTileDrops");
        public static BooleanRule DoWeatherCycle = new BooleanRule("doWeatherCycle");
        public static BooleanRule KeepInventory = new BooleanRule("keepInventory");
        public static BooleanRule LogAdminCommands = new BooleanRule("logAdminCommands");
        public static IntRule MaxCommandChainLength = new IntRule("maxCommandChainLength");
        public static IntRule MaxEntityCramming = new IntRule("maxEntityCramming");
        public static BooleanRule MobGriefing = new BooleanRule("mobGriefing");
        public static BooleanRule NaturalRegeneration = new BooleanRule("naturalRegeneration");
        public static IntRule RandomTickSpeed = new IntRule("randomTickSpeed");
        public static BooleanRule ReducedDebugInfo = new BooleanRule("reducedDebugInfo");
        public static BooleanRule SendCommandFeedback = new BooleanRule("sendCommandFeedback");
        public static BooleanRule ShowDeathMessages = new BooleanRule("showDeathMessages");
        public static IntRule SpawnRadius = new IntRule("spawnRadius");
        public static BooleanRule SpectatorsGenerateChunks = new BooleanRule("spectatorsGenerateChunks");

        protected static Dictionary<string, GameRule> Registry;

        public GameRule(string id) : base(id)
        {
            if(Registry == null)
            {
                Registry = new Dictionary<string, GameRule>();
            }
            Registry.Add(id, this);
        }

        public static implicit operator GameRule(string id)
        {
            return Get(id, Registry);
        }

        public ResultCommand Get()
        {
            return new ResultCommand($"gamerule {this}",Storage.Result);
        }
    }

    public class BooleanRule : GameRule
    {
        public BooleanRule(string id) : base(id)
        {
        }

        public static implicit operator BooleanRule(string id)
        {
            return Get(id, Registry) as BooleanRule;
        }

        public void Set(bool value)
        {
            FunctionWriter.Write($"gamerule {this} {value}");
        }
    }

    public class IntRule : GameRule
    {
        public IntRule(string id) : base(id)
        {
        }

        public static implicit operator IntRule(string id)
        {
            return Get(id, Registry) as IntRule;
        }

        public void Set(int value)
        {
            FunctionWriter.Write($"gamerule {this} {value}");
        }
    }
}
