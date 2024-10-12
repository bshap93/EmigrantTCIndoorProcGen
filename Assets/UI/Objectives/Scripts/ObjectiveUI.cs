using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts
{
    public class ObjectiveUI : MonoBehaviour
    {
        static readonly int Active = Animator.StringToHash("Active");
        [FormerlySerializedAs("_objectiveText")] [SerializeField]
        TMP_Text objectiveText;
        [FormerlySerializedAs("_animator")] [SerializeField]
        Animator animator;

        Objective _currentObjective;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);
        }

        // Update is called once per frame
        public void OnObjectiveCompleted(Objective objective)
        {
            Debug.Log("Objective Completed: " + objective.objectiveText);
            animator.SetBool(Active, false);
            _currentObjective.isActive = false;
            _currentObjective = null;
            objectiveText.text = string.Empty;
        }
        public void OnObjectiveAssigned(Objective objective)
        {
            Debug.Log("Objective Assigned: " + objective.objectiveText);
            _currentObjective = objective;
            _currentObjective.isActive = true;
            animator.SetBool(Active, true);
            objectiveText.text = objective.objectiveText;
        }
    }
}