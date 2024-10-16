using System;
using Core.Events;
using UI.Objectives.Scripts;
using UnityEngine;

namespace Environment.Interactables.Scripts
{
    public abstract class InteractableHint : MonoBehaviour
    {
        public Objective objective;

        void OnEnable()
        {
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);
            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
        }
        void OnObjectiveCompleted(Objective arg0)
        {
            throw new NotImplementedException();
        }
        void OnObjectiveAssigned(Objective arg0)
        {
            throw new NotImplementedException();
        }
    }
}
