using Characters.Player.Scripts;
using Characters.Scripts;
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
            if (equippedSlot != null) equippedSlot.OnContentsChanged.AddListener(OnEquippedItemChanged);
            equippedSlot = PlayerCharacter.Instance.gameObject.GetComponentInChildren<EquippedSlot>();
            itemWorldFragmentManager = GameManager.Instance.itemWorldFragmentManager;
        }

        void OnEquippedItemChanged(ItemStack arg0)
        {
            if (arg0.ID != default && arg0.ID == currentItemObejct.ID)
                if (consumableCategory.Contains(arg0.ID))
                {
                    var item = itemWorldFragmentManager.GetItemByID(arg0.ID);

                    Equip(item, PlayerCharacter.Instance);
                }
        }


        public override void Equip(BaseItemObject item, IDamageable equipper)
        {
            Debug.Log("Equipping " + item.name);
            currentItemObejct = item;
            PlayerCharacter.Instance.equippedItem = item;
            PlayerCharacter.Instance.equippableHandler = this;
        }

        public override void Unequip(BaseItemObject item, IDamageable equipper)
        {
        }
    }
}
