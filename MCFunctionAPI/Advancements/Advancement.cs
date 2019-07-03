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

        public Advancement Parent { get; set; }

        public Reward Reward { get; set; }

        private Dictionary<string, Trigger> Criteria = new Dictionary<string, Trigger>();

        private List<string[]> requirements;

        public Advancement(ResourceLocation id)
        {
            Id = id;
            ShowToast = true;
            AnnounceToChat = true;
            Hidden = false;
            Frame = FrameType.Task;
        }

        public Advancement OneOf(params Trigger[] orGroup)
        {
            
            if (requirements == null)
            {
                requirements = new List<string[]>();
                foreach (string k in Criteria.Keys)
                {
                    requirements.Add(new string[] { k });
                }
            }
            List<string> names = new List<string>();
            foreach (var t in orGroup)
            {
                string n = NameTrigger(t);
                Criteria.Add(n, t);
                names.Add(n);
            }
            requirements.Add(names.ToArray());
            return this;
        }

        private string NameTrigger(Trigger t)
        {
            string name = t.SuggestName();
            if (name == null)
            {
                name = t.Id.Path;
            }
            if (Criteria.ContainsKey(name))
            {
                int i = 1;
                string unique = name + 1;
                while (Criteria.ContainsKey(unique))
                {
                    i++;
                    unique = name + i;
                }
                name = unique;
            }
            return name;
        }

        public Advancement AddTrigger(Trigger trigger)
        {
            return AddTrigger(NameTrigger(trigger), trigger);
        }

        public Advancement AddTrigger(string id, Trigger trigger)
        {
            Criteria.Add(id, trigger);
            if (requirements != null)
            {
                requirements.Add(new string[] { id });
            }
            return this;
        }


        public Advancement OnBreedAnimal(EntityCondition child = null, EntityCondition parent = null, EntityCondition partner = null)
        {
            return AddTrigger(new AnimalsBred(child,parent,partner));
        }

        public string ToJson()
        {
            NBT display = new NBT()
                .Set("title", Title)
                .Set("description", Description)
                .Set("icon", new NBT().Set("item", Icon?.Id).Set("nbt", Icon == null ? null : Icon.nbt.IsEmpty() ? null : Icon.nbt.ToString()))
                .Set("frame", Frame.ToString().ToLower())
                .Set("show_toast", ShowToast)
                .Set("announce_to_chat", AnnounceToChat)
                .Set("hidden", Hidden);
            NBT crits = new NBT();
            foreach (var e in Criteria)
            {
                crits.Set(e.Key, new NBT().Set("trigger", e.Value.Id).Set("conditions", e.Value));
            }
            return new NBT().Set("display", display).Set("parent",Parent.Id).Set("criteria", crits).SetAny("requirements", requirements).Set("rewards",Reward).ToString(true, true);
        }

        public static Advancement Build(ResourceLocation id,
            TextComponent title = null,
            TextComponent description = null,
            Item icon = null,
            FrameType frame = FrameType.Task,
            bool toast = true,
            bool chat = true,
            bool hide = false,
            Advancement parent = null,
            Reward reward = null)
        {
            return new Advancement(id)
            {
                Title = title,
                Description = description,
                Icon = icon,
                Frame = frame,
                ShowToast = toast,
                AnnounceToChat = chat,
                Hidden = hide,
                Parent = parent,
                Reward = reward
            };
        }

        public static implicit operator Advancement(string s)
        {
            return new Advancement(s);
        }

        public static implicit operator Advancement(ResourceLocation id)
        {
            return new Advancement(id);
        }

        public Advancement Create()
        {
            Id.Namespace.AddAdvancement(this);
            return this;
        }


    }

    public enum FrameType
    {
        Task,
        Goal,
        Challenge
    }

    public class HiddenAdvancement : Advancement
    {
        public HiddenAdvancement(ResourceLocation id) : base(id)
        {
            AnnounceToChat = false;
            Hidden = true;
            ShowToast = false;
        }
    }

}
