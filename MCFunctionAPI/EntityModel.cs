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
    public class EntityModel
    {

        public string tag;
        public EntityType type;
        public NBT nbt;
        

        public EntityModel(string tag, EntityType type)
        {
            this.type = type;
            this.tag = tag;
            this.nbt = new NBT();
        }

        public void Create(Position pos)
        {
            FunctionWriter.Write($"summon {type} {pos} {nbt.Copy().Set("Tags",new string[] {tag})}");
        }

        public void Create(Position pos, Action<Entities> action)
        {
            Objective _age = FunctionWriter.EnsureAgeObjective();
            _age[EntitySelector.AllEntities.Is(type).Tag(tag)]++;
            Create(pos);
            _age[EntitySelector.AllEntities.Is(type).Tag(tag)]++;
            EntitySelector.AllEntities.Is(type).Score(_age, 1).Execute.Run(action);
        }

        public EntitySelector CreateAndGet(Position pos)
        {
            Objective _age = FunctionWriter.EnsureAgeObjective();
            _age[EntitySelector.AllEntities.Is(type).Tag(tag)]++;
            Create(pos);
            _age[EntitySelector.AllEntities.Is(type).Tag(tag)]++;
            return EntitySelector.AllEntities.Is(type).Tag(tag).Score(_age, 1).LimitTo(1);
        }

        public EntitySelector Selector
        {
            get
            {
                return EntitySelector.AllEntities.Is(type).Tag(tag);
            }
        }

    }
}
