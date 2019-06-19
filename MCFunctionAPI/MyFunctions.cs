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
    [Root]
    public class MyFunctions : FunctionContainer
    {
        

        public static void Setup()
        {
            SetWeather(Weather.Clear);
            EntitySelector selector = new EntitySelector("@a") { Level = "1..5", Gamemode = 0 };
            selector.Tags.Has("cool");
            execute.As(selector).Run().Title(TextComponent.Of(Segment.Text("yoooo")));
            RunFunction(BrettyEfes);
            SetBlock("~ ~ ~5", "stone");
            Clone(Position.Here, "~1 ~1 ~-1", "0 1 0", MaskMode.Filtered.With("diamond_block"));
            Time += 500;

        }

        public const string a = "hi";

        [Desc(a)]
        public static void BrettyEfes()
        {
            EntitySelector selector = EntitySelector.Target("@p");
            selector.Distance = "5..10";
            selector.Gamemode = 3;
            selector.Limit = 5;

            selector.Scores.Where("myObj", 5);

            EntitySelector selector2 = EntitySelector.Target("@p").InDistance("5..10").InGamemode(3).LimitTo(5).Score("myObj", 5);

            EntitySelector selector3 = "@p[distance=5..10,gamemode=spectator,limit=5,scores={myObj=5}]";

                
            execute.As(selector).Store(Storage.Result, "Shlomo", "myObj").Run().XP.Levels.Query();
            execute.As(selector2).Store(Storage.Result, "Shlomo", "myObj").Run().XP.Levels.Query();
            execute.As(selector3).Store(Storage.Result, "Shlomo", "myObj").Run().XP.Levels.Query();


            Console.WriteLine(GetFirstRawCommand((c) => Give("diamond")));

            RunFunction(Blocks.Hello);

        }

        public class Blocks
        {

            public static void Hello()
            {
                Say("Hello!");
            }

        }

    }
}
