using Polyperfect.Crafting.Integration;
using Polyperfect.Crafting.Integration.UGUI;
using UI.Scripts;
using UnityEngine;

namespace Items.Crafting.Scripts
{
    public class CrafterInitializer : MonoBehaviour
    {
        public UGUICrafter crafter;
        // Start is called before the first frame update
        void Start()
        {
            var hotbarController = FindObjectOfType<HotbarController>();
            GameObject hotbar;

            if (hotbarController != null)
            {
                hotbar = hotbarController.gameObject;
                // crafter.StartingInventory = hotbar.GetComponent<ChildSlotsInventory>();
                // crafter.StartingOutput = hotbar.GetComponent<ChildSlotsInventory>();
            }
        }
    }
}
