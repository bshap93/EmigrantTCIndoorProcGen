using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using Core.GameManager.Scripts;
using Polyperfect.Crafting.Demo;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Equipment.Consumables
{
    public abstract class ConsumableHandler : EquippableHandler
    {
        public CategoryObject consumableCategory;
        void Start()
        {
            if (EquippedSlot != null) EquippedSlot.OnContentsChanged.AddListener(OnEquippedItemChanged);
            EquippedSlot = PlayerCharacter.Instance.gameObject.GetComponentInChildren<EquippedSlot>();
            ItemWorldFragmentManager = GameManager.Instance.itemWorldFragmentManager;
        }

        void OnEquippedItemChanged(ItemStack arg0)
        {
            if (arg0.ID != default && arg0.ID == currentItemObejct.ID)
                if (consumableCategory.Contains(arg0.ID))
                {
                    var item = ItemWorldFragmentManager.GetItemByID(arg0.ID);

                    Equip(item, PlayerCharacter.Instance);
                }
        }


        public override void Equip(BaseItemObject item, IDamageable equipper)
        {
            Debug.Log("Equipping " + item.name);
            EventManager.EOnEquippedItem.Invoke(item);
            currentItemObejct = item;
            PlayerCharacter.Instance.equippedItem = item;
            PlayerCharacter.Instance.equippableHandler = this;
        }

        public override void Unequip(BaseItemObject item, IDamageable equipper)
        {
        }
    }
}
