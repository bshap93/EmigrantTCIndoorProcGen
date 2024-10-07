using Core.Events;
using Environment.LevelGeneration.Rooms.Scripts;
using UnityEngine;

namespace Characters.Player.Managers.Scripts
{
    public class PlayerStartManager : MonoBehaviour
    {
        public GameObject player; // Reference to your player object
        public string startPointName = "StartPoint"; // The name of the start point object in the start room


        void Start()
        {
            if (RoomManager.Instance != null)
                EventManager.EOnRoomGeneration.AddListener(OnRoomGenerated);
            else
                Debug.LogError("Listener not added!");

            if (player == null)
                player = GameObject.FindWithTag("Player");
        }

        // Match the correct signature
        void OnRoomGenerated()
        {
            // Find the start point by name in the scene after generation is complete
            var startPoint = GameObject.Find(startPointName);
            if (startPoint != null)
            {
                // Move the player to the start point's position
                player.transform.position = startPoint.transform.position;
                player.transform.rotation = startPoint.transform.rotation;
            }
            else
            {
                Debug.LogWarning("Start point not found in the generated dungeon.");
            }
        }
    }
}
