using Characters.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawnManager : MonoBehaviour
{
    public enum Hand
    {
        Left,
        Right,
        Both
    }

    public GameObject pistolPrefab;
    public GameObject axePrefab;
    public GameObject extinguisherPrefab;

    [FormerlySerializedAs("_playerCharacter")]
    public PlayerCharacter playerCharacter;

    GameObject _currentLeftHandItem;
    GameObject _currentRightHandItem;

    Transform _leftHandItemHolder;
    Transform _rightHandItemHolder;

    void Start()
    {
        _leftHandItemHolder = transform.Find("LeftHand/ItemHolder");
        _rightHandItemHolder = transform.Find("RightHand/ItemHolder");
    }

    public void SpawnItem(GameObject itemPrefab, Hand hand = Hand.Right)
    {
        if (hand == Hand.Both)
            SpawnTwoHandedItem(itemPrefab);
        else
            SpawnSingleHandItem(itemPrefab, hand);
    }

    void SpawnSingleHandItem(GameObject itemPrefab, Hand hand)
    {
        var itemHolder = hand == Hand.Left ? _leftHandItemHolder : _rightHandItemHolder;
        ref var currentItem = ref hand == Hand.Left ? ref _currentLeftHandItem : ref _currentRightHandItem;

        // Unequip previous item
        if (currentItem != null)
        {
            var previousItem = currentItem.GetComponent<IUsableItem>();
            previousItem?.Unequip();
            Destroy(currentItem);
        }

        // Instantiate and set up new item
        currentItem = Instantiate(itemPrefab, itemHolder);
        AlignItemToHand(currentItem, itemHolder);

        // Equip the item
        var usableItem = currentItem.GetComponent<IUsableItem>();
        usableItem?.Equip();
        playerCharacter.SetCurrentItem(usableItem, hand);
    }

    void SpawnTwoHandedItem(GameObject itemPrefab)
    {
        // Unequip previous items
        if (_currentLeftHandItem != null)
        {
            var previousItem = _currentLeftHandItem.GetComponent<IUsableItem>();
            previousItem?.Unequip();
            Destroy(_currentLeftHandItem);
        }

        if (_currentRightHandItem != null)
        {
            var previousItem = _currentRightHandItem.GetComponent<IUsableItem>();
            previousItem?.Unequip();
            Destroy(_currentRightHandItem);
        }

        // Instantiate and set up new item in the right hand
        _currentRightHandItem = Instantiate(itemPrefab, _rightHandItemHolder);
        AlignItemToHand(_currentRightHandItem, _rightHandItemHolder);

        // Set up left hand IK target
        var leftHandIKTarget = _currentRightHandItem.transform.Find("LeftHandIKTarget");
        if (leftHandIKTarget != null) playerCharacter.SetLeftHandIKTarget(leftHandIKTarget);

        // Equip the item
        var usableItem = _currentRightHandItem.GetComponent<IUsableItem>();
        usableItem?.Equip();
        playerCharacter.SetCurrentItem(usableItem, Hand.Both);
    }

    void AlignItemToHand(GameObject item, Transform itemHolder)
    {
        var gripPosition = item.transform.Find("GripPosition");
        if (gripPosition != null)
        {
            itemHolder.position = gripPosition.position;
            itemHolder.rotation = gripPosition.rotation;

            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("GripPosition not found on item.");
        }
    }
}
