using Characters.Player.InputHandlers.Scripts;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Interactables.Scripts
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public enum InteractableType
        {
            Container,
            Door
        }

        [FormerlySerializedAs("InteractionUI")]
        public GameObject interactionUI;

        public string objectName;

        public InteractableType interactableType;
        BoxCollider _zoneCollider;

        void Start()
        {
            // Get the Collider from the child zone (assuming there's a single child collider)
            _zoneCollider = GetComponent<BoxCollider>();

            // Ensure it's marked as a trigger
            if (_zoneCollider != null)
                _zoneCollider.isTrigger = true;
        }

        // Trigger detection for when the player enters the zone
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                // Notify the player they can interact with this object
                SimpleInteractInputHandler.Instance.SetInteractable(this);
        }

        // Trigger detection for when the player exits the zone
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Clear the reference when the player leaves the range
                SimpleInteractInputHandler.Instance.ClearInteractable();
                EndInteractionSimple();
            }
        }

        public void InteractSimple()
        {
            UIManager.Instance.inGameConsoleManager
                .LogMessage("Interacting with object: " + objectName);

            if (interactableType == InteractableType.Container) interactionUI.SetActive(true);
        }

        public void EndInteractionSimple()
        {
            if (interactableType == InteractableType.Container)
            {
                interactionUI.SetActive(false);
                Debug.Log("End interaction with container");
            }
        }
    }
}
