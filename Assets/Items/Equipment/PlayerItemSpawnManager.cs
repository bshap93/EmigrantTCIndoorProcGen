using System;
using System.Collections.Generic;
using Characters.Player.Scripts;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Equipment
{
    public class PlayerItemSpawnManager : MonoBehaviour
    {
        public Transform handTransform;
        public List<ItemData> itemDataList;

        GameObject _currentItem;

        void Start()
        {
            foreach (var itemData in itemDataList) itemData.itemName = itemData.baseItemObject.name;
        }


        public void SpawnItem(string itemName)
        {
            if (_currentItem != null) Destroy(_currentItem);

            var itemData = itemDataList.Find(item => item.itemName == itemName);
            if (itemData != null)
            {
                _currentItem = Instantiate(itemData.itemPrefab, handTransform);
                _currentItem.transform.localPosition = itemData.positionOffset;
                _currentItem.transform.localRotation = Quaternion.Euler(itemData.rotationOffset);
                
                Debug.Log($"Item {itemName} spawned.");

                // Activate the corresponding behavior script
                var equippableHandler = _currentItem.GetComponent<EquippableHandler>();
                if (equippableHandler != null)
                    equippableHandler.Equip(equippableHandler.currentItemObejct, PlayerCharacter.Instance);
            }
            else
            {
                Debug.LogWarning($"Item {itemName} not found in the item list.");
            }
        }

        [Serializable]
        public class ItemData
        {
            public string itemName;
            public GameObject itemPrefab;
            public Vector3 positionOffset;
            public Vector3 rotationOffset;
            public BaseItemObject baseItemObject;
        }
    }
}
