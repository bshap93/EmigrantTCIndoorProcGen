using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts
{
    public class ObjectiveSkippedHook : MonoBehaviour
    {
        [FormerlySerializedAs("_skippedObjective")]
        public Objective skippedObjective;
        ObjectiveManager _objectiveManager;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _objectiveManager = FindObjectOfType<ObjectiveManager>();
                var currentObjective = _objectiveManager.GetCurrentObjective();
                if (currentObjective.objectiveId == skippedObjective.objectiveId)
                    _objectiveManager.CompleteCurrentObjective();
            }
        }
    }
}
