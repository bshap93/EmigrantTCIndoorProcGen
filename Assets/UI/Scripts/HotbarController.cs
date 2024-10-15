using System.Collections.Generic;
using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;
using UI.Objectives.Scripts;
using UI.Objectives.Scripts.ObjectiveTypes;
using UnityEngine;

namespace UI.Scripts
{
    public class HotbarController : InventoryController
    {
        readonly Dictionary<GameObject, UGUITransferableItemSlot> _slots = new();

        void OnEnable()
        {
            var slots = GetComponentsInChildren<UGUITransferableItemSlot>();
            foreach (var slot in slots) _slots[slot.gameObject] = slot;
        }
        public void AddItemToFirstEmptySlot(ItemStack itemStack)
        {
            foreach (var slot in _slots.Values)
                if (slot.Peek().ID == default)
                {
                    slot.InsertCompletely(itemStack);
                    CheckIfObjectiveIsCompletedByItemTransfer(itemStack);
                    return;
                }
        }
        public void CheckIfObjectiveIsCompletedByItemTransfer(ItemStack itemStack)
        {
            var objectiveManager = FindObjectOfType<ObjectiveManager>();

            if (objectiveManager != null)
            {
                var currentObjective = objectiveManager.GetCurrentObjective();
                if (currentObjective is ObtainItemObjective itemObjective)
                    itemObjective.CheckIfObjectiveIsCompletedByItemTransfer(itemStack);
            }
        }
    }
}
