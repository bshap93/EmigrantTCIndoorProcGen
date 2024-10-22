using Core.Events;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes

{
    [CreateAssetMenu(fileName = "Data", menuName = "EquipItemObjective", order = 1)]
    public class EquipItemObjective : Objective
    {
        public BaseItemObject itemToEquip;
        public EquipItemObjective(string objectiveText) : base(objectiveText)
        {
        }

        void OnEnable()
        {
            EventManager.EOnEquippedItem.AddListener(OnItemEquipped);
        }
        void OnItemEquipped(BaseItemObject arg0)
        {
            if (arg0.ID == itemToEquip.ID)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
