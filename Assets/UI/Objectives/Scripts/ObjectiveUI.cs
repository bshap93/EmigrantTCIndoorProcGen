using System.Collections.Generic;
using Core.Events;
using Environment.Interactables.Scripts;
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

        public Dictionary<Objective, GameObject> HintParticleSystems;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);

            HintParticleSystems = new Dictionary<Objective, GameObject>();
            var hints = FindObjectsOfType<InteractableGlowHint>();
            foreach (var hint in hints)
                if (hint != null)
                {
                    HintParticleSystems[_currentObjective] = hint.glowParticle;
                    if (hint.objective == null)
                        Debug.LogError("InteractableGlowHint " + hint.name + " does not have an objective assigned!");
                    else
                        Debug.Log("Added hint for " + hint.objective.objectiveText);
                }
        }

        // Update is called once per frame
        public void OnObjectiveCompleted(Objective objective)
        {
            if (objective.name == _currentObjective.name)
            {
                animator.SetBool(Active, false);
                _currentObjective.isActive = false;
                _currentObjective.isCompleted = true;
                _currentObjective = null;
                objectiveText.text = string.Empty;
                objectiveManager.CompleteCurrentObjective();

                if (HintParticleSystems.ContainsKey(objective))
                    HintParticleSystems[objective].SetActive(false);
            }
        }
        public void OnObjectiveAssigned(Objective objective)
        {
            _currentObjective = objective;
            _currentObjective.isActive = true;
            _currentObjective.isCompleted = false;
            animator.SetBool(Active, true);
            objectiveText.text = objective.objectiveText;

            if (HintParticleSystems.ContainsKey(objective))
                HintParticleSystems[objective].SetActive(true);
        }
    }
}
