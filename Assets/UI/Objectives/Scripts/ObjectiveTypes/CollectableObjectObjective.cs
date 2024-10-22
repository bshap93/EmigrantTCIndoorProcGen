using Core.Events;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "CollectItemObjective", order = 1)]
    public class CollectableObjectObjective : Objective
    {
        public string objectName;
        public CollectableObjectObjective(string objectiveText) : base(objectiveText)
        {
        }

        void OnEnable()
        {
            EventManager.EOnCollectableCollected.AddListener(OnObjectCollected);
            hasLocation = true;
        }
        void OnObjectCollected(string objName)
        {
            CheckIfObjectiveIsCompletedByCollect(objName);
        }

        void CheckIfObjectiveIsCompletedByCollect(string objName)
        {
            if (objName == objectName)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
