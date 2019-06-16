using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{
    public class Advancement
    {
        public ResourceLocation Id { get; }
        public TextComponent Title { get; set; }
        public TextComponent Description { get; set; }
        public Item Icon { get; set; }
        private Dictionary<string, Trigger> Criteria;
        private bool Grouping;

        public Advancement(ResourceLocation id)
        {
            Id = id;
        }

        public Advancement OrGroup()
        {
            Grouping = true;
            return this;
        }

        public Advancement And()
        {
            Grouping = false;
            return this;
        }

        public Advancement AddTrigger<D>(Trigger<D> trigger) where D : Delegate
        {
            Criteria.Add(trigger.GetName(),trigger);
            return this;
        }

        public Advancement OnBreedAnimal(OnBreedAnimals onBreed)
        {
            AnimalsBredTrigger trigger = new AnimalsBredTrigger(onBreed);
            return AddTrigger(trigger);
        }
        

    }

}
