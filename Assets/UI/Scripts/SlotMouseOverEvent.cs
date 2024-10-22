using Core.GameManager.Scripts;
using Items.Inventory.Scripts;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Scripts
{
    public class SlotMouseOverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        ItemStack _contents;
        InventoryController _inventoryController;
        ItemWorldFragmentManager _itemWorldFragmentManager;
        UGUITransferableItemSlot _slot;
        bool _tooltipActive;
        GameObject _tooltipInstance;
        GameObject _tooltipPrefab;


        // Start is called before the first frame update
        void Start()
        {
            _slot = GetComponent<UGUITransferableItemSlot>();
            _contents = _slot.Peek();
            _itemWorldFragmentManager = GameManager.Instance.itemWorldFragmentManager;
            _tooltipActive = false;
            _inventoryController = GetComponentInParent<InventoryController>();
            _tooltipPrefab = _inventoryController.tooltipPrefab;
        }

        // Update is called once per frame
        void Update()
        {
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            var item = _itemWorldFragmentManager.GetItemByID(_contents.ID);
            if (item != null)
                ShowTooltip(item);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_tooltipActive) HideTooltip();
        }
        void ShowTooltip(BaseItemObject item)
        {
            var instancePosition = transform.position + new Vector3(
                _inventoryController.tooltipOffsetX, _inventoryController.tooltipOffsetY, 0);

            _tooltipInstance = Instantiate(_tooltipPrefab, instancePosition, Quaternion.identity);

            _tooltipInstance.transform.SetParent(transform);
            Debug.Log("Tooltip shown at offest position: " + _tooltipInstance.transform.position);
            // _tooltipInstance.GetComponent<Tooltip>().SetItem(item);
            _tooltipActive = true;
        }

        void HideTooltip()
        {
            if (_tooltipInstance != null)
                Destroy(_tooltipInstance);

            _tooltipActive = false;
        }
    }
}
