using Characters.Scripts;
using Items.Inventory.Scripts;
using Polyperfect.Crafting.Demo;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Equipment
{
    public abstract class EquippableHandler : MonoBehaviour
    {
        public BaseItemObject currentItemObejct;
        public bool hideUntilReadyToUse;
        protected EquippedSlot EquippedSlot;
        protected ItemWorldFragmentManager ItemWorldFragmentManager;
        public abstract void Equip(BaseItemObject item, IDamageable equipper);
        public abstract void Unequip(BaseItemObject item, IDamageable equipper);

        public abstract void Use(IDamageable target);

        public abstract void CeaseUsing();
    }
}
