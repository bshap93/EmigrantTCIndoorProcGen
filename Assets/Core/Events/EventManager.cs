using Environment.Interactables.Scripts;
using Polyperfect.Crafting.Integration;
using UI.Objectives.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Events
{
    public class EventManager : MonoBehaviour
    {
        // Pausing the game loop and entering a pause state overlay
        public static readonly UnityEvent EPauseGame = new();
        // Resuming the game loop and exiting the pause state overlay
        public static readonly UnityEvent EResumeGame = new();
        // Character has died, enter a game over state overlay

        // Damage has been dealt to a character by name by a certain amount

        public static readonly UnityEvent ERestartCurrentLevel = new();


        public static readonly UnityEvent EOnRoomGeneration = new();

        public static readonly UnityEvent EPlayerEnteredRoom = new();

        // Objective Events

        public static readonly UnityEvent<Objective> EOnObjectiveCompleted = new();

        public static readonly UnityEvent<Objective> EOnObjectiveAssigned = new();

        public static readonly UnityEvent<GameObject> EOnObjectDestroyed = new();

        // Interactable Object Events

        public static readonly UnityEvent<InteractableObject> EOnObjectInteracted = new();

        public static readonly UnityEvent<InteractableObject> EOnObjectEndInteracted = new();

        public static readonly UnityEvent<string> EOnItemUsed = new();

        public static readonly UnityEvent<string> EOnCollectableCollected = new();

        public static readonly UnityEvent<BaseItemObject> EOnEquippedItem = new();
    }
}
