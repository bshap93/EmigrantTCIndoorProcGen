﻿using Characters.Player.InputHandlers.Scripts;
using UnityEngine;

namespace Environment.Interactables.Scripts
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        Collider _zoneCollider;

        void Start()
        {
            // Get the Collider from the child zone (assuming there's a single child collider)
            _zoneCollider = GetComponent<Collider>();

            // Ensure it's marked as a trigger
            if (_zoneCollider != null)
                _zoneCollider.isTrigger = true;
        }

        // Trigger detection for when the player enters the zone
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Notify the player they can interact with this object
                SimpleInteractInputHandler.Instance.SetInteractable(this);
                UnityEngine.Debug.Log("Player can interact with object: " + gameObject.name);
            }
        }

        // Trigger detection for when the player exits the zone
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                // Clear the reference when the player leaves the range
                SimpleInteractInputHandler.Instance.ClearInteractable();

            UnityEngine.Debug.Log("Player left the range of object: " + gameObject.name);
        }

        public void InteractSimple()
        {
            UnityEngine.Debug.Log("Interacting with object: " + gameObject.name);
            // Interaction logic goes here
        }
    }
}