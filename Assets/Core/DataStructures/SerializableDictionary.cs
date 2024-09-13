using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DataStructures
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] List<TKey> keys = new();
        [SerializeField] List<TValue> values = new();

        // Save the dictionary to lists before serialization
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // Load dictionary from lists after deserialization
        public void OnAfterDeserialize()
        {
            Clear();

            if (keys.Count != values.Count)
                throw new Exception(
                    $"There are {keys.Count} keys and {values.Count} values after deserialization. Make sure that both key and value types are serializable.");

            for (var i = 0; i < keys.Count; i++) this[keys[i]] = values[i];
        }
    }
}
