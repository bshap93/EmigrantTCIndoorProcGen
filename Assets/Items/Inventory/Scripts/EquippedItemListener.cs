using Items.Equipment;
using Polyperfect.Crafting.Demo;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items.Inventory.Scripts
{
    public class EquippedItemListener : MonoBehaviour
    {
        public EquippedSlot equippedSlot;
        [FormerlySerializedAs("ItemWorldFragmentManager")]
        public ItemWorldFragmentManager itemWorldFragmentManager;
        public PlayerItemSpawnManager playerItemSpawnManager;


        void Start()
        {
            if (equippedSlot != null) equippedSlot.OnContentsChanged.AddListener(OnEquippedItemChanged);
            if (itemWorldFragmentManager == null)
                itemWorldFragmentManager = FindObjectOfType<ItemWorldFragmentManager>();
            if (playerItemSpawnManager == null)
                playerItemSpawnManager = FindObjectOfType<PlayerItemSpawnManager>();
        }

        void OnDestroy()
        {
            // Unsubscribe to prevent memory leaks
            if (equippedSlot != null) equippedSlot.OnContentsChanged.RemoveListener(OnEquippedItemChanged);
        }

        void OnEquippedItemChanged(ItemStack newItemStack)
        {
            if (newItemStack.ID != default)
            {
                // Fetch the item using the itemWorldFragmentManager
                var item = itemWorldFragmentManager.GetItemByID(newItemStack.ID);
                playerItemSpawnManager.SpawnItem(item.name);
            }
        }
    }
}
