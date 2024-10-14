using System.Collections.Generic;
using Core.Events;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    public class DestroyObjective : Objective
    {
        public int objectsToDestroyCount;
        public string objectTag;
        public Dictionary<string, int> NeededNumberToDestroyByTag = new();
        public Dictionary<string, int> NumDestroyedObjectsByTag = new();

        public DestroyObjective(string objectiveText) : base(objectiveText)
        {
            EventManager.EOnObjectDestroyed.AddListener(OnObjectDestroyed);
        }
        void OnObjectDestroyed(GameObject destroyedGameObject)
        {
            if (destroyedGameObject.CompareTag(objectTag))
            {
                // objectsToDestroyCount--;
                // if (objectsToDestroyCount <= 0)
                // {
                //     isCompleted = true;
                //     EventManager.EOnObjectiveCompleted.Invoke(this);
                // }
            }
        }
    }
}
