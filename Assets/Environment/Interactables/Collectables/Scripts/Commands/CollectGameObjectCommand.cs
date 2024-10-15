using Core.Utilities.Commands;
using Polyperfect.Crafting.Integration;
using UI.Scripts;
using UnityEngine;

namespace Environment.Interactables.Collectables.Scripts.Commands
{
    public class CollectGameObjectCommand : ISimpleCommand
    {
        readonly GameObject _collectableGameObject;
        readonly HotbarController _hotbarController;
        readonly ItemStack _itemStack;


        public CollectGameObjectCommand(GameObject collectableGameObject, HotbarController hotbarController,
            ItemStack itemStack)
        {
            _collectableGameObject = collectableGameObject;
            _hotbarController = hotbarController;
            _itemStack = itemStack;
        }

        public void Execute()
        {
            _collectableGameObject.SetActive(false);
            _hotbarController.AddItemToFirstEmptySlot(_itemStack);
        }
    }
}
