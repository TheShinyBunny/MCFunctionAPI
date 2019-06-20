using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity.Models
{
    public class MobModel<T> : EntityModelBase<T> where T : MobModel<T>
    {

        protected new T This;

        public T SetHealth(float health, bool max)
        {
            Set("Health", health);
            if (max)
            {
                List<NBT> attributes = Get<List<NBT>>("Attributes");
                if (attributes == null)
                {
                    attributes = new List<NBT>();
                }
                attributes.Add(new NBT().Set("Name", "generic.maxHealth").Set("Base",(double)health));
                Set("Attributes", attributes);
            }
            return This;
        }

        public T AddEffect(EffectInstance effect)
        {
            List<EffectInstance> effects = Get<List<EffectInstance>>("ActiveEffects");
            if (effects == null)
            {
                effects = new List<EffectInstance>();
            }
            effects.Add(effect);
            Set("ActiveEffects", effects);
            return This;
        }

        public T SetItemInSlot(EquipmentSlot slot, Item item, int count = 1)
        {
            if (slot.armor)
            {
                List<NBT> armorItems = Get<List<NBT>>("ArmorItems");
                if (armorItems == null)
                {
                    armorItems = Enumerable.Repeat(new NBT(), 4).ToList();
                }
                armorItems[slot.index] = new NBT().Set("id", item.Id).Set("Count", count).Set("tag", item.nbt);
                Set("ArmorItems", armorItems);
            } else
            {
                List<NBT> handItems = Get<List<NBT>>("HandItems");
                if (handItems == null)
                {
                    handItems = Enumerable.Repeat(new NBT(), 2).ToList();
                }
                handItems[slot.index] = new NBT().Set("id", item.Id).Set("Count", count).Set("tag", item.nbt);
                Set("HandItems", handItems);
            }
            return This;
        }

        public T NoAI()
        {
            Set("NoAI",true);
            return This;
        }

    }
}
