using Characters.Player.Scripts;
using Characters.Scripts;
using Core.GameManager.Scripts;
using Items.Equipment;
using Polyperfect.Crafting.Demo;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items.Weapons
{
    public abstract class WeaponHandler : EquippableHandler
    {
        public Transform firePoint;
        [FormerlySerializedAs("objectCategory")]
        public CategoryObject weaponCategory;
        public CategoryWithFloat damageCategory;
        [FormerlySerializedAs("currentEquippableItemObject")]
        [FormerlySerializedAs("currentWeapon")]
        [FormerlySerializedAs("_weaponObject")]
        public GameObject weaponObject;

        float _dmaage;

        void Start()
        {
            if (equippedSlot != null) equippedSlot.OnContentsChanged.AddListener(OnEquippedItemChanged);
            equippedSlot = PlayerCharacter.Instance.gameObject.GetComponentInChildren<EquippedSlot>();
            itemWorldFragmentManager = GameManager.Instance.itemWorldFragmentManager;
        }
        void OnEquippedItemChanged(ItemStack arg0)
        {
            if (arg0.ID != default && arg0.ID == currentItemObejct.ID)
            {
                if (weaponCategory.Contains(arg0.ID))
                {
                    var item = itemWorldFragmentManager.GetItemByID(arg0.ID);

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
            Debug.Log("Equipping " + item.name);
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
