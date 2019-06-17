using MCFunctionAPI.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{

    public interface ITrigger
    {
        ResourceLocation Id { get; }

        NBT RunEvent();
        
    }

    public abstract class Trigger<D> : ITrigger where D : Delegate
    {

        protected D del;
        private string name;

        public abstract ResourceLocation Id { get; }

        public Trigger(D del)
        {
            this.del = del;
        }

        public Trigger(string name, D del)
        {
            this.name = name;
            this.del = del;
        }

        public void SetName(string name)
        {
            if (name == null)
            {
                this.name = name;
            }
        }

        public string GetName()
        {
            return name;
        }

        public abstract NBT RunEvent();
    }
    

    public delegate void OnBreedAnimals(EntityCondition child, EntityCondition parent, EntityCondition partner);

    public class AnimalsBredTrigger : Trigger<OnBreedAnimals>
    {
        public AnimalsBredTrigger(OnBreedAnimals del) : base(del)
        {

        }

        public AnimalsBredTrigger(string name, OnBreedAnimals del) : base(name,del)
        {

        }

        public override ResourceLocation Id => "bred_animals";

        public override NBT RunEvent()
        {
            EntityCondition child = new EntityCondition();
            EntityCondition parent = new EntityCondition();
            EntityCondition partner = new EntityCondition();
            del(child, parent, partner);
            SetName("bred_" + (child.Type == null ? "animal" : child.Type.id));
            return new NBT().Set("child", child).Set("parent", parent).Set("partner", partner);
        }
    }
    
}
