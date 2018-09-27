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
    public class MyFunctions : FunctionContainer
    {

        Objective test = "test";

        public void Loop()
        {
            SetWeather(Weather.Clear);
            EntitySelector selector = new EntitySelector("@a") { Level = "1..5", Gamemode = 0 };
            selector.Tags.And("cool");
            execute.As(selector).Run.Title(TextComponent.Of(Segment.Text("yoooo")));
            RunFunction("subfolder/brettyefes");
            RunFunction(Hello);
            SetBlock("~ ~ ~5", "stone");
            Clone(Position.Here, "~1 ~1 ~-1", "0 1 0", MaskMode.Filtered.With("diamond_block"));
            Time += 500;
            test.Create("dummy");
        }

        public void Hello()
        {
            Say("hello world!");
            test[EntitySelector.AllEntities()] = 5;
            test["shlomo"] += 20;
            test["menachem"] = test["yehuda"];
            test["ShoonyBoony"].Swap(test["SfogAdomAtaShforferet"]);

            EntitySelector selector = new EntitySelector("@e")
            {
                Type = "pig",
                Tags = "cool",
                Scores = new ScoreSet().Where("foo", 5).Where("bar", "1.."),
                Team = Team.Any
            };
        }

        public class SubFolder : FunctionContainer
        {

            public void BrettyEfes()
            {
                EntitySelector selector = EntitySelector.Target("@p");
                selector.Distance = "5..10";
                selector.Gamemode = 3;
                selector.Limit = 5;
                selector.Scores.Where("myObj", 5);

                EntitySelector selector2 = EntitySelector.Target("@p").InDistance("5..10").InGamemode(3).LimitTo(5).Score("myObj", 5);

                EntitySelector selector3 = "@p[distance=5..10,gamemode=spectator,limit=5,scores={myObj=5}]";

                
                execute.As(selector).Store(Storage.Result, "Shlomo", "myObj").Run.XP.Levels.Query();
                execute.As(selector2).Store(Storage.Result, "Shlomo", "myObj").Run.XP.Levels.Query();
                execute.As(selector3).Store(Storage.Result, "Shlomo", "myObj").Run.XP.Levels.Query();


                Console.WriteLine(GetFirstRawCommand((c) => c.Give("diamond")));

            }

        }

    }
}
