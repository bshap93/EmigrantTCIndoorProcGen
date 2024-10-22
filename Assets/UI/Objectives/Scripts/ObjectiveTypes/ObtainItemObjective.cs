using Core.Events;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "ItemObjective", order = 1)]
    public class ObtainItemObjective : Objective
    {
        public ItemStack itemStack;
        public ObtainItemObjective(string objectiveText, ItemStack itemStack) : base(objectiveText)
        {
            this.itemStack = itemStack;
        }

        public void CheckIfObjectiveIsCompletedByItemTransfer(ItemStack itemStack)
        {
            if (itemStack == this.itemStack)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
