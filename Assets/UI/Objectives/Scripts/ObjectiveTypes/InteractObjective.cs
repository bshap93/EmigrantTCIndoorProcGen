using Core.Events;
using Environment.Interactables.Scripts;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "InteractableObjective", order = 1)]
    public class InteractObjective : Objective
    {
        public string interactableObjectName;
        public InteractObjective(string objectiveText) : base(objectiveText)
        {
        }
        void OnEnable()
        {
            EventManager.EOnObjectInteracted.AddListener(OnObjectInteracted);
            hasLocation = true;
        }
        void OnObjectInteracted(InteractableObject interactableObj)
        {
            CheckIfObjectiveIsCompletedByInteract(interactableObj);
        }

        public void CheckIfObjectiveIsCompletedByInteract(InteractableObject interactableObj)
        {
            if (interactableObj.objectName == interactableObjectName)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
