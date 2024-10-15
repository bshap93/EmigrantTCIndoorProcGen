using Core.Events;
using UnityEngine;

namespace Environment.Interactables.Scripts
{
    public class InteractableGlowHint : MonoBehaviour
    {
        public GameObject glowParticle;
        InteractableObject _interactableObject;

        void Start()
        {
            _interactableObject = GetComponent<InteractableObject>();
        }

        void OnEnable()
        {
            EventManager.EOnObjectInteracted.AddListener(OnObjectInteracted);
            glowParticle.SetActive(true);
        }

        void OnDisable()
        {
            EventManager.EOnObjectInteracted.RemoveListener(OnObjectInteracted);
        }
        void OnObjectInteracted(InteractableObject interactableObject)
        {
            if (interactableObject.name == _interactableObject.name)
            {
                glowParticle.SetActive(false);
                enabled = false;
            }
        }
        // Start is called before the first frame update

        // Update is called once per frame
    }
}
