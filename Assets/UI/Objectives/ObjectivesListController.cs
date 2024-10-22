using System.Collections.Generic;
using TMPro;
using UI.Objectives.Scripts;
using UnityEngine;

namespace UI.Objectives
{
    public class ObjectivesListController : MonoBehaviour
    {
        static int _currentObjective;
        static readonly int Active = Animator.StringToHash("Active");
        [SerializeField] GameObject objectiveOne;
        [SerializeField] GameObject objectiveTwo;
        [SerializeField] GameObject objectiveThree;

        [SerializeField] TMP_Text objectiveTextOne;
        [SerializeField] TMP_Text objectiveTextTwo;
        [SerializeField] TMP_Text objectiveTextThree;
        // Start is called before the first frame update

        [SerializeField] Animator
            objectiveOneAnimator;

        public List<Objective> objectives;


        void Start()
        {
            objectiveTextOne.text = objectives[_currentObjective].objectiveText;
            objectiveTwo.SetActive(false);
            objectiveThree.SetActive(false);
        }

        public void CompleteCurrentObjective()
        {
            objectives[_currentObjective].isCompleted = true;
            objectiveOneAnimator.SetBool(Active, true);

            StartCoroutine(ShowNextObjectiveIfAvailable());
        }

        IEnumerator<WaitForSeconds> ShowNextObjectiveIfAvailable()
        {
            yield return new WaitForSeconds(3);
            if (_currentObjective + 1 < objectives.Count)
            {
                _currentObjective++;
                objectiveTextOne.text = objectives[_currentObjective].objectiveText;
                objectiveOneAnimator.SetBool(Active, false);
            }
        }
    }
}
