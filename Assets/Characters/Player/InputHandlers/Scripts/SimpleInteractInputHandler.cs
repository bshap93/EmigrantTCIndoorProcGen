using Environment.Interactables.Buttons.Scripts.Commands;
using Environment.Interactables.Scripts;
using UnityEngine;

namespace Characters.Player.InputHandlers.Scripts
{
    // Simple input handler for interacting with a single object
    public class SimpleInteractInputHandler : MonoBehaviour
    {
        // Currently handling only a single interactable object at a time
        IInteractable _currentInteractable; // Store the interactable object
        InteractSimpleCommand _interactSimpleCommand;

        public static SimpleInteractInputHandler Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F) && _currentInteractable != null)
            {
                _interactSimpleCommand = new InteractSimpleCommand();
                _interactSimpleCommand.Execute(_currentInteractable); // Execute interaction on the stored interactable
            }
        }

        // Set the interactable object when the player is near
        public void SetInteractable(IInteractable interactable)
        {
            _currentInteractable = interactable;
        }

        // Clear the interactable object when the player leaves the area
        public void ClearInteractable()
        {
            _currentInteractable = null;
        }
    }
}
