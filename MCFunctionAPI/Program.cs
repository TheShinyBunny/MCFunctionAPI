using MCFunctionAPI.Advancements;
using MCFunctionAPI.Blocks;
using MCFunctionAPI.Entity;
using MCFunctionAPI.LootTables;
using MCFunctionAPI.Scoreboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCFunctionAPI
{
    class Program : Datapack
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Namespace main = p.CreateNamespace("main");
            main.AddFunctions(typeof(MyFunctions));

            /*
            Advancement test = new Advancement("main:mytab/test")
            {
                Title = "Pig Breeder",
                Description = "Breed two pigs together",
                Icon = "carrot"
            }
            .OnBreedAnimal((child, parent, partner) => { child.Type = EntityType.Pig; parent.NBT = "{foo:\"bar\"}"; });
            */
            
            LootTable skeleton = new LootTable("skeleton",TableType.Entity)
            {
                new Pool()
                {
                    new Entry("arrow").SetCount("0..2").LootingBonus("0..1")
                },
                new Pool()
                {
                    new Entry("bone").SetCount("0..2").LootingBonus("0..1")
                },
                new Pool()
                {
                    new Entry("skeleton_skull").KilledByPlayer().RandomChance(0.08f,0.08f)
                }
            };
            p.AddVanillaLootTable(skeleton);

        }

        public override string GetDescription()
        {
            return "Hello World";
        }

        public override string GetName()
        {
            return "test";
        }
    }
}
