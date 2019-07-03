using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI
{

    /// <summary>
    /// A compiled instance of a function. All lines are added, and at the end the file is written.
    /// </summary>
    public class MCFunction
    {

        public ResourceLocation Id { get; set; }
        public List<KeyValuePair<ScoreEventHandler, MCFunction>> ScoreHandlers = new List<KeyValuePair<ScoreEventHandler,MCFunction>>();

        private List<string> Lines = new List<string>();

        private bool written;

        public MCFunction(ResourceLocation id)
        {
            Id = id;
        }

        public void AddLine(string line)
        {
            Lines.Add(line);
            if (written)
            {
                File.AppendAllLines(Id.Namespace.Path + "/functions/" + Id.Path + ".mcfunction", Lines);
            }
        }

        public void WriteFile()
        {
            Directory.CreateDirectory(Id.Namespace.Path + "/functions/" + Id.ParentPath);
            File.WriteAllLines(Id.Namespace.Path + "/functions/" + Id.Path + ".mcfunction",Lines);
            written = true;
            Lines.Clear();
        }

        public void AddScoreTick(KeyValuePair<ScoreEventHandler, MCFunction> e)
        {
            if (!ScoreHandlers.Contains(e))
            {
                ScoreHandlers.Add(e);
                AddLine($"execute as @e[scores={{{e.Key.Objective}={e.Key.TargetValue}}}] at @s run function {e.Value.Id}");
            }
        }

        public void AddScoreCreation(KeyValuePair<ScoreEventHandler, MCFunction> e)
        {
            if (!ScoreHandlers.Contains(e))
            {
                ScoreHandlers.Add(e);
                AddLine($"scoreboard objectives add {e.Key.Objective} {e.Key.Criteria}");
            }
        }

        public void AddScoreCreation(Objective obj, string criteria)
        {
            AddLine($"scoreboard objectives add {obj} {criteria}");
        }
    }
}
