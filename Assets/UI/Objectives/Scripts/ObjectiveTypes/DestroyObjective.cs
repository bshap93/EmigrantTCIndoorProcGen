using Core.Events;
using Core.Spawning.Scripts;
using UnityEngine;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "DestroyObjective", order = 1)]
    public class DestroyObjective : Objective
    {
        public int objectsToDestroyCount;
        public string objectTag;
        DestroyManager _destroyManager;


        public DestroyObjective(string objectiveText) : base(objectiveText)
        {
        }


        void OnEnable()
        {
            if (_destroyManager == null) _destroyManager = FindObjectOfType<DestroyManager>();
            EventManager.EOnObjectDestroyed.AddListener(OnObjectDestroyed);
            _destroyManager.NeededNumberToDestroyByTag[objectTag] = objectsToDestroyCount;
            _destroyManager.AddTag(objectTag, objectsToDestroyCount);
        }

        void OnDisable()
        {
            EventManager.EOnObjectDestroyed.RemoveListener(OnObjectDestroyed);
        }
        void OnObjectDestroyed(GameObject destroyedGameObject)
        {
            if (destroyedGameObject.CompareTag(objectTag)) _destroyManager.IncrementDestroyedObject(objectTag);


            CheckIfObjectiveIsCompleted();
        }
        public void CheckIfObjectiveIsCompleted()
        {
            var objects = GameObject.FindGameObjectsWithTag(objectTag);
            if (objects.Length == 0)
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
