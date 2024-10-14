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
        [FormerlySerializedAs("_objectiveManager")] [SerializeField]
        ObjectiveManager objectiveManager;

        Objective _currentObjective;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);

            Debug.Log(_currentObjective);
        }

        // Update is called once per frame
        public void OnObjectiveCompleted(Objective objective)
        {
            animator.SetBool(Active, false);
            _currentObjective.isActive = false;
            _currentObjective.isCompleted = true;
            _currentObjective = null;
            objectiveText.text = string.Empty;
            objectiveManager.CompleteCurrentObjective();
        }
        public void OnObjectiveAssigned(Objective objective)
        {
            Debug.Log("Objective Assigned: " + objective.objectiveText);
            _currentObjective = objective;
            _currentObjective.isActive = true;
            _currentObjective.isCompleted = false;
            animator.SetBool(Active, true);
            objectiveText.text = objective.objectiveText;
        }
    }
}
