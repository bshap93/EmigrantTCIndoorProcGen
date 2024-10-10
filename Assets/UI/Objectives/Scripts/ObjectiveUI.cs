using Core.Events;
using UnityEngine;

namespace UI.Objectives.Scripts
{
    public class ObjectiveUI : MonoBehaviour
    {
        Objective _currentObjective;
        // Start is called before the first frame update
        void Start()
        {
            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);
        }

        // Update is called once per frame
        public void OnObjectiveCompleted(Objective objective)
        {
            Debug.Log("Objective Completed: " + objective.objectiveText);
        }
        public void OnObjectiveAssigned(Objective objective)
        {
            Debug.Log("Objective Assigned: " + objective.objectiveText);
        }
        
        
    }
}
