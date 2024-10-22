using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Inventory.Scripts
{
    public class ItemWorldFragmentManager : MonoBehaviour
    {
        public ItemWorldFragment itemWorldFragment; // Reference to your ItemWorldFragment asset

        // Method to get item based on RuntimeID
        public BaseItemObject GetItemByID(RuntimeID id)
        {
            foreach (var item in itemWorldFragment.ItemObjects)
                if (item.ID.Equals(id))
                    return item; // Return the item that matches the ID

            return null;
        }
    }
}
