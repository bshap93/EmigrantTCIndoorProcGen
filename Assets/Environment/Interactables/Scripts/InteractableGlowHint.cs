using Core.Events;
using UI.Objectives.Scripts;
using UnityEngine;

namespace Environment.Interactables.Scripts
{
    public class InteractableGlowHint : MonoBehaviour
    {
        public GameObject glowParticle;
        public Objective objective;

        void Start()
        {
        }

        void OnEnable()
        {
            EventManager.EOnObjectiveAssigned.AddListener(OnObjectiveAssigned);
            EventManager.EOnObjectiveCompleted.AddListener(OnObjectiveCompleted);
        }

        void OnDisable()
        {
            EventManager.EOnObjectiveAssigned.RemoveListener(OnObjectiveAssigned);
            EventManager.EOnObjectiveCompleted.RemoveListener(OnObjectiveCompleted);
        }
        void OnObjectiveCompleted(Objective arg0)
        {
            if (arg0.objectiveId == objective.objectiveId) glowParticle.SetActive(false);
        }
        void OnObjectiveAssigned(Objective arg0)
        {
            Debug.Log("Objective assigned: " + arg0.objectiveId);
            if (arg0.objectiveId == objective.objectiveId) glowParticle.SetActive(true);
        }

        // Start is called before the first frame update

        // Update is called once per frame
    }
}
