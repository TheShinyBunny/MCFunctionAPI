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
        public FrameType Frame { get; set; }

        public bool ShowToast { get; set; }
        public bool AnnounceToChat { get; set; }

        public bool Hidden { get; set; }

        private Dictionary<string, ITrigger> Criteria = new Dictionary<string, ITrigger>();
        private bool Grouping;

        public Advancement(ResourceLocation id)
        {
            Id = id;
            ShowToast = true;
            AnnounceToChat = true;
            Hidden = false;
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

        public string ToJson()
        {
            NBT display = new NBT()
                .Set("title", Title)
                .Set("description", Description)
                .Set("icon", new NBT().Set("item", Icon.Id).Set("nbt", Icon.nbt.IsEmpty() ? null : Icon.nbt.ToString()))
                .Set("frame", Frame.ToString().ToLower())
                .Set("show_toast", ShowToast)
                .Set("announce_to_chat", AnnounceToChat)
                .Set("hidden", Hidden);
            return new NBT().Set("display", display).ToString(true,true);
        }

        public enum FrameType
        {
            Task,
            Goal,
            Challenge
        }
        

    }

}
