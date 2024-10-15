using Core.Events;
using Core.Utilities.Commands;
using Environment.Interactables.Collectables.Scripts.Commands;
using Environment.Interactables.Scripts;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Environment.Interactables.Collectables.Scripts
{
    public class CollectableObject : MonoBehaviour, ICollectable, IInteractable
    {
        public enum CollectableState
        {
            NotCollected,
            Collected
        }

        public enum CollectableType
        {
            OxygenBottle,
            GameObject
        }

        public CollectableType collectableType;
        public GameObject collectableGameObject;

        public GameObject tooltipPrefab;
        public string objectName;

        [CanBeNull] public GameObject interactionUI;

        BoxCollider _zoneCollider;


        protected ISimpleCommand CollectGameObjectCommand;

        protected ISimpleCommand CollectOxygenBottleCommand;
        protected CollectableState CurrentState;

        bool playerInRange;

        GameObject tooltipInstance;

        Canvas uiCanvas;

        public bool IsCollected => CurrentState == CollectableState.Collected;

        void Start()
        {
            uiCanvas = UIManager.Instance.uiCanvas;

            if (uiCanvas == null)
            {
                Debug.LogError("UI Canvas not found in scene");
                return;
            }

            _zoneCollider = GetComponent<BoxCollider>();

            if (_zoneCollider != null)
                _zoneCollider.isTrigger = true;


            CollectGameObjectCommand = new CollectGameObjectCommand();
            CollectOxygenBottleCommand = new CollectOxygenBottleCommand(collectableGameObject);
        }

        void Update()
        {
            if (playerInRange)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    InteractSimple();
                    HideTooltip();
                }

                UpdateTooltipPosition();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                ShowTooltip();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                HideTooltip();
            }
        }

        public void CollectObject()
        {
            if (IsCollected)
                return;

            if (collectableType == CollectableType.OxygenBottle)
                CollectOxygenBottleCommand.Execute();
            else
                CollectGameObjectCommand.Execute();

            SetState(CollectableState.Collected);

            EventManager.EOnCollectableCollected.Invoke(objectName);
        }
        public void SetState(CollectableState state)
        {
            CurrentState = state;
        }
        public void InteractSimple()
        {
            UIManager.Instance.inGameConsoleManager.LogMessage("Player collected " + objectName);
            CollectObject();
        }

        public void ShowTooltip()
        {
            if (tooltipInstance == null && tooltipPrefab != null && uiCanvas != null)
            {
                // Instantiate the tooltip as a child of the Canvas
                tooltipInstance = Instantiate(tooltipPrefab, uiCanvas.transform);

                // Set the initial position of the tooltip
                UpdateTooltipPosition();
            }
        }

        void UpdateTooltipPosition()
        {
            if (tooltipInstance != null)
            {
                // Convert the object's position to screen space
                var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

                // Set the tooltip's position
                tooltipInstance.GetComponent<RectTransform>().position = screenPosition;
            }
        }

        void HideTooltip()
        {
            if (tooltipInstance != null)
            {
                Destroy(tooltipInstance);
                tooltipInstance = null;
            }
        }
    }
}
