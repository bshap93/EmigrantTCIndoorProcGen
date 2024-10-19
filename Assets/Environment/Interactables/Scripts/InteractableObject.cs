using Characters.Player.InputHandlers.Scripts;
using Core.Events;
using Core.Events.EventManagers;
using Environment.Interactables.Openable.Scripts;
using Environment.Interactables.Triggerables.Scripts;
using JetBrains.Annotations;
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
            Door,
            Display,
            BreakableDoorPanel,
            CraftingStation,
            Console,
            LevelHatch,
            Triggerable,
            PowerNode,
            Npc
        }

        [SerializeField] [CanBeNull] OpenableObject openableObject;

        [FormerlySerializedAs("InteractionUI")] [CanBeNull]
        public GameObject interactionUI;

        [CanBeNull] [SerializeField] TriggerableObject triggerableObject;

        public bool broken;

        public string objectName;

        public GameObject tooltipPrefab;

        public InteractableType interactableType;

        // public UnityEvent<InteractableObject> onInteract;
        // public UnityEvent<InteractableObject> onEndInteract;


        PlayerEventManager _playerEventManager;

        bool _playerInRange;

        GameObject _tooltipInstance;

        Canvas _uiCanvas;
        Collider _zoneCollider;

        void Start()
        {
            _uiCanvas = UIManager.Instance.uiCanvas;

            if (_uiCanvas == null)
            {
                Debug.LogError("UI Canvas not found in scene");
                return;
            }

            _playerEventManager = GameObject.Find("Player").GetComponent<PlayerEventManager>();

            // onInteract = new UnityEvent<InteractableObject>();
            // onEndInteract = new UnityEvent<InteractableObject>();


            EventManager.EOnObjectInteracted.AddListener(OnInteractHandler);
            EventManager.EOnObjectEndInteracted.AddListener(OnEndInteractHandler);

            // onEndInteract.AddListener(OnEndInteractHandler);


            triggerableObject = GetComponent<TriggerableObject>();


            // Get the Collider from the child zone (assuming there's a single child collider)
            _zoneCollider = GetComponent<BoxCollider>();

            // Ensure it's marked as a trigger
            if (_zoneCollider != null)
                _zoneCollider.isTrigger = true;

            EndInteractionSimple();
        }

        void Update()
        {
            if (_playerInRange)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (openableObject != null && openableObject.IsClosed)
                    {
                        if (openableObject != null)
                            openableObject.Open();

                        InteractSimple();
                        HideTooltip();
                    }
                    else if (openableObject != null)
                    {
                        if (openableObject != null)
                            openableObject.Close();

                        EndInteractionSimple();
                    }

                    if (triggerableObject != null) triggerableObject.Trigger();
                }

                UpdateTooltipPosition();
            }
        }

        // Trigger detection for when the player enters the zone
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Notify the player they can interact with this object
                SimpleInteractInputHandler.Instance.SetInteractable(this);

                _playerInRange = true;
                ShowTooltip();
            }
        }

        // Trigger detection for when the player exits the zone
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Clear the reference when the player leaves the range
                SimpleInteractInputHandler.Instance.ClearInteractable();
                EndInteractionSimple();
                _playerInRange = false;
                HideTooltip();

                if (openableObject != null) openableObject.Close();
            }
        }

        public void InteractSimple()
        {
            UIManager.Instance.inGameConsoleManager
                .LogMessage("Interacting with object: " + objectName);


            EventManager.EOnObjectInteracted.Invoke(this);

            if (interactableType == InteractableType.Container)
                if (interactionUI != null)
                    interactionUI.SetActive(true);

            if (interactableType == InteractableType.LevelHatch)
                if (interactionUI != null)
                    interactionUI.SetActive(true);
        }


        void ShowTooltip()
        {
            if (_tooltipInstance == null && tooltipPrefab != null && _uiCanvas != null)
            {
                // Instantiate the tooltip as a child of the Canvas
                _tooltipInstance = Instantiate(tooltipPrefab, _uiCanvas.transform);

                // Set the initial position of the tooltip
                UpdateTooltipPosition();
            }
        }

        void HideTooltip()
        {
            if (_tooltipInstance != null)
            {
                Destroy(_tooltipInstance);
                _tooltipInstance = null;
            }
        }

        void UpdateTooltipPosition()
        {
            if (_tooltipInstance != null)
                // Convert the object's position to screen space
                if (Camera.main != null)
                {
                    var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

                    // Set the tooltip's position
                    _tooltipInstance.GetComponent<RectTransform>().position = screenPosition;
                }
        }

        public void EndInteractionSimple()
        {
            EventManager.EOnObjectEndInteracted.Invoke(this);
            if (interactableType == InteractableType.Container)
                if (interactionUI != null)
                    interactionUI.SetActive(false);

            if (interactableType == InteractableType.CraftingStation)
            {
                if (interactionUI != null) interactionUI.SetActive(false);
                Debug.Log("End interaction with crafting station");
            }

            if (interactableType == InteractableType.LevelHatch)
                if (interactionUI != null)
                    interactionUI.SetActive(false);
        }

        void OnInteractHandler(InteractableObject interactableObject)
        {
            _playerEventManager.TriggerPlayerInteracted(interactableObject);
        }

        void OnEndInteractHandler(InteractableObject interactableObject)
        {
            _playerEventManager.TriggerPlayerEndedInteraction(interactableObject);
        }
    }
}
