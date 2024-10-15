using Core.Events;
using Core.Utilities.Commands;
using Environment.Interactables.Collectables.Scripts.Commands;
using Environment.Interactables.Scripts;
using JetBrains.Annotations;
using Polyperfect.Crafting.Integration;
using UI;
using UI.Scripts;
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
            GameObject,
            FireExtinguisher
        }

        public CollectableType collectableType;
        public GameObject collectableGameObject;

        public GameObject tooltipPrefab;
        public string objectName;

        [CanBeNull] public GameObject interactionUI;

        public ItemStack itemStack;

        HotbarController _hotbarController;

        bool _playerInRange;

        GameObject _tooltipInstance;

        Canvas _uiCanvas;

        BoxCollider _zoneCollider;


        protected ISimpleCommand CollectGameObjectCommand;

        protected ISimpleCommand CollectOxygenBottleCommand;
        protected CollectableState CurrentState;

        public bool IsCollected => CurrentState == CollectableState.Collected;

        void Start()
        {
            _uiCanvas = UIManager.Instance.uiCanvas;

            _hotbarController = FindObjectOfType<HotbarController>();

            if (_uiCanvas == null)
            {
                Debug.LogError("UI Canvas not found in scene");
                return;
            }

            _zoneCollider = GetComponent<BoxCollider>();

            if (_zoneCollider != null)
                _zoneCollider.isTrigger = true;


            CollectGameObjectCommand = new CollectGameObjectCommand(
                collectableGameObject, _hotbarController, itemStack);

            CollectOxygenBottleCommand = new CollectOxygenBottleCommand(collectableGameObject);
        }

        void Update()
        {
            if (_playerInRange)
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
                _playerInRange = true;
                ShowTooltip();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
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
            if (_tooltipInstance == null && tooltipPrefab != null && _uiCanvas != null)
            {
                // Instantiate the tooltip as a child of the Canvas
                _tooltipInstance = Instantiate(tooltipPrefab, _uiCanvas.transform);

                // Set the initial position of the tooltip
                UpdateTooltipPosition();
            }
        }

        void UpdateTooltipPosition()
        {
            if (_tooltipInstance != null)
            {
                // Convert the object's position to screen space
                var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

                // Set the tooltip's position
                _tooltipInstance.GetComponent<RectTransform>().position = screenPosition;
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
    }
}
