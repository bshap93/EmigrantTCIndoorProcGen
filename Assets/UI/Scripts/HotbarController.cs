using Polyperfect.Crafting.Integration;
using UI.Objectives.Scripts.ObjectiveTypes;

namespace UI.Scripts
{
    public class HotbarController : InventoryController
    {
        void Start()
        {
            // Debug.Log("HotbarController Start");
        }

        public void CheckIfObjectiveIsCompletedByItemTransfer(ItemStack itemStack)
        {
            var objectiveManager = FindObjectOfType<ObjectiveManager>();

            if (objectiveManager != null)
            {
                var currentObjective = objectiveManager.GetCurrentObjective();
                if (currentObjective is ItemObjective itemObjective)
                    itemObjective.CheckIfObjectiveIsCompletedByItemTransfer(itemStack);
            }
        }
    }
}
