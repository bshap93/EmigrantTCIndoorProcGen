using Core.Events;
using Core.Events.EventManagers;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "UseItemObjective", order = 1)]
    public class UseItemObjective : Objective
    {
        public string itemUsedId;
        PlayerEventManager _eventManager;
        public UseItemObjective(string objectiveText) : base(objectiveText)
        {
        }

        void OnEnable()
        {
            EventManager.EOnItemUsed.AddListener(OnItemUsed);
        }
        void OnItemUsed(string itemUsed)
        {
            if (itemUsed == itemUsedId)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
