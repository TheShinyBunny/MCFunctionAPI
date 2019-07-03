using MCFunctionAPI.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Advancements
{

    public abstract class IBaseTriggerBuilder
    {
        public Advancement MakeAdvancement(ResourceLocation id,
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
            return Create(Advancement.Build(id, title, description, icon, frame, toast, chat, hide, parent, reward));
        }

        public void RunFunction(Advancement a, Namespace ns, CommandWrapper.Function f)
        {
            a.Reward = Reward.Function(ns, f);
            Create(a);
        }

        public void RunFunction(Advancement a, Namespace ns, CommandWrapper.ParameterFunction f, string param)
        {
            a.Reward = Reward.Function(ns, f, param);
            Create(a);
        }

        public abstract Advancement Create(Advancement a);
    }

    public abstract class Trigger : OrTriggerList, INBTSerializable
    {
        public abstract ResourceLocation Id { get; }

        public abstract NBT ToNBT();

        public abstract string SuggestName();

        public static PlacedBlock OnPlacedBlock(Block b = null, ItemCondition used = null, LocationCondition location = null)
        {
            return new PlacedBlock(b, used, location);
        }

        object INBTSerializable.ToNBT()
        {
            return ToNBT();
        }

        public static OrTriggerList OneOf(params Trigger[] triggers)
        {
            return new OrTriggerList(triggers);
        }

        public static AndTriggerList AllOf(params OrTriggerList[] triggers)
        {
            return new AndTriggerList(triggers);
        }

        public override Trigger[] GetTriggers()
        {
            return new Trigger[] { this };
        }

        public override Advancement Create(Advancement a)
        {
            return a.AddTrigger(this).Create();
        }
    }

    public class OrTriggerList : IBaseTriggerBuilder
    {

        private Trigger[] triggers;

        public OrTriggerList(params Trigger[] triggers)
        {
            this.triggers = triggers;
        }

        public virtual Trigger[] GetTriggers()
        {
            return triggers;
        }
        

        public override Advancement Create(Advancement a)
        {
            return a.OneOf(triggers).Create();
        }
    }

    public class AndTriggerList : IBaseTriggerBuilder
    {

        private OrTriggerList[] triggers;

        public AndTriggerList(params OrTriggerList[] triggers)
        {
            this.triggers = triggers;
        }

        public override Advancement Create(Advancement a)
        {
            foreach (OrTriggerList or in triggers)
            {
                a.OneOf(or.GetTriggers());
            }
            return a.Create();
        }
    }

    public class AnimalsBred : Trigger
    {
        public EntityCondition Child { get; set; }
        public EntityCondition Parent { get; set; }
        public EntityCondition Partner { get; set; }

        public AnimalsBred(EntityCondition child = null, EntityCondition parent = null, EntityCondition partner = null)
        {
            Child = child;
            Parent = parent;
            Partner = partner;
        }

        public override ResourceLocation Id => "animals_bred";

        public override string SuggestName()
        {
            return Parent != null && Parent.Type != null ? "bred_" + Parent.Type.Id : null;
        }

        public override NBT ToNBT()
        {
            return new NBT().Set("child", Child).Set("parent", Parent).Set("partner",Partner);
        }
    }

    public class PlacedBlock : Trigger
    {
        public override ResourceLocation Id => "placed_block";

        public Block Block { get; set; }
        public ItemCondition ItemUsed { get; set; }
        public LocationCondition Location { get; set; }

        public PlacedBlock(Block block = null, ItemCondition itemUsed = null, LocationCondition location = null)
        {
            Block = block;
            ItemUsed = itemUsed;
            Location = location;
        }

        public override string SuggestName()
        {
            return Block == null ? null : "placed_" + Block.Id.Path;
        }

        public override NBT ToNBT()
        {
            return new NBT().Set("block", Block?.Id).Set("item", ItemUsed).Set("location", Location).Set("state", Block?.state);
        }
    }

}
