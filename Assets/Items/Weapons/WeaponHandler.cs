using Characters.Player.Scripts;
using Characters.Scripts;
using Core.GameManager.Scripts;
using Items.Equipment;
using Polyperfect.Crafting.Demo;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Weapons
{
    public abstract class WeaponHandler : EquippableHandler
    {
        public Transform firePoint;
        public CategoryObject weaponCategory;

        public GameObject weaponObject;

        float _damage;

        void Start()
        {
            if (EquippedSlot != null) EquippedSlot.OnContentsChanged.AddListener(OnEquippedItemChanged);
            var playerCharacter = FindObjectOfType<PlayerCharacter>();
            EquippedSlot = playerCharacter.gameObject.GetComponentInChildren<EquippedSlot>();
            ItemWorldFragmentManager = GameManager.Instance.itemWorldFragmentManager;
        }
        void OnEquippedItemChanged(ItemStack arg0)
        {
            if (arg0.ID != default && arg0.ID == currentItemObejct.ID)
            {
                if (weaponCategory.Contains(arg0.ID))
                {
                    var item = ItemWorldFragmentManager.GetItemByID(arg0.ID);

                    Equip(item, PlayerCharacter.Instance);
                }
            }
            else if (arg0.ID == default)
            {
                Unequip(currentItemObejct, PlayerCharacter.Instance);
            }
        }

        public override void Equip(BaseItemObject item, IDamageable equipper)
        {
            weaponObject.SetActive(true);
            PlayerCharacter.Instance.equippedItem = item;
            PlayerCharacter.Instance.equippableHandler = this;
        }
        public override void Unequip(BaseItemObject item, IDamageable equipper)
        {
            weaponObject.SetActive(false);
            PlayerCharacter.Instance.equippedItem = null;
            PlayerCharacter.Instance.equippableHandler = null;
        }
    }
}
