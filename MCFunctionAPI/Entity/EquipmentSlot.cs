using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCFunctionAPI.Entity
{
    public class EquipmentSlot : EnumBase
    {
        public static EquipmentSlot MainHand = new EquipmentSlot("weapon.mainhand",false,0);
        public static EquipmentSlot OffHand = new EquipmentSlot("weapon.offhand", false,1);
        public static EquipmentSlot Feet = new EquipmentSlot("armor.feet", true,0);
        public static EquipmentSlot Legs = new EquipmentSlot("armor.legs", true,1);
        public static EquipmentSlot Chest = new EquipmentSlot("armor.chest", true,2);
        public static EquipmentSlot Head = new EquipmentSlot("armor.head", true,3);

        public readonly bool armor;
        public readonly int index;
        private static Dictionary<string, EquipmentSlot> Registry;

        public EquipmentSlot(string id, bool armor, int index) : base(id)
        {
            if (Registry == null) Registry = new Dictionary<string, EquipmentSlot>();
            Registry.Add(id, this);
            this.armor = armor;
            this.index = index;
        }


        public static implicit operator EquipmentSlot(string s)
        {
            return Get(s, Registry);
        }
    }
}
