using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "Objectives", order = 1)]
    public abstract class Objective : ScriptableObject
    {
        [FormerlySerializedAs("IsCompleted")] public bool isCompleted;
        [FormerlySerializedAs("ObjectiveText")]
        public bool isActive;
        public string objectiveText;
        public bool hasLocation;
        [CanBeNull] public Transform location;
        public string objectiveId;


        protected Objective(string objectiveText)
        {
            this.objectiveText = objectiveText;
            isCompleted = false;
            isActive = false;
        }
    }
}
