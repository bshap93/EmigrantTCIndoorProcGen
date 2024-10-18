using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts
{
    public class ObjectiveManager : MonoBehaviour
    {
        public List<Objective> objectives;
        public ObjectiveUI objectiveUI;

        [FormerlySerializedAs("_currentObjectiveIndex")] [SerializeField]
        int currentObjectiveIndex;
        public GameObject glowEffect;
        Objective _currentObjective;
        void Start()
        {
            objectiveUI.OnObjectiveAssigned(objectives[currentObjectiveIndex]);
            _currentObjective = objectives[currentObjectiveIndex];
        }
        void OnEnable()
        {
            foreach (var objective in objectives)
            {
                objective.isActive = false;
                objective.isCompleted = false;
            }
        }

        public void CompleteCurrentObjective()
        {
            _currentObjective.isCompleted = true;

            TryGetNextObjective();
        }
        void TryGetNextObjective()
        {
            if (currentObjectiveIndex + 1 < objectives.Count)
            {
                currentObjectiveIndex++;
                _currentObjective = objectives[currentObjectiveIndex];
                objectiveUI.OnObjectiveAssigned(_currentObjective);
            }
            else
            {
                Debug.Log("All objectives completed!");
            }
        }
        public Objective GetCurrentObjective()
        {
            return _currentObjective;
        }
    }
}
