using Core.Events;
using Core.Spawning.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts.ObjectiveTypes
{
    [CreateAssetMenu(fileName = "Data", menuName = "DestroyObjective", order = 1)]
    public class DestroyObjective : Objective
    {
        public int objectsToDestroyCount;
        public string objectTag;

        [FormerlySerializedAs("_destroyManager")] [SerializeField]
        DestroyManager destroyManager;

        public DestroyObjective(string objectiveText) : base(objectiveText)
        {
        }

        void OnEnable()
        {
            destroyManager = FindObjectOfType<DestroyManager>();
            EventManager.EOnObjectDestroyed.AddListener(OnObjectDestroyed);
            // destroyManager.NeededNumberToDestroyByTag[objectTag] = objectsToDestroyCount;
            destroyManager.AddTag(objectTag, objectsToDestroyCount);
            Debug.Log("DestroyObjective OnEnable");
        }
        void OnObjectDestroyed(GameObject destroyedGameObject)
        {
            if (destroyedGameObject.CompareTag(objectTag)) destroyManager.IncrementDestroyedObject(objectTag);

            CheckIfObjectiveIsCompleted();
        }
        public void CheckIfObjectiveIsCompleted()
        {
            if (destroyManager.CheckIfObjectiveIsComplete(objectTag))
            {
                isCompleted = true;
                EventManager.EOnObjectiveCompleted.Invoke(this);
            }
        }
    }
}
