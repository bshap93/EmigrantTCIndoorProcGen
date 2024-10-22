using System.Collections.Generic;
using Characters.Player.Scripts;
using Core.Events;
using DunGen;
using Sirenix.Utilities;
using UnityEngine;

namespace Environment.LevelGeneration.Rooms.Scripts
{
    public class RoomManager : MonoBehaviour
    {
        public Room[] rooms;
        int _roomCounter;
        RuntimeDungeon _runtimeDungeon;

        public static RoomManager Instance { get; set; }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep the manager across scenes
            }
            else
            {
                Destroy(gameObject); // Prevent duplicates
            }

            // Attempt to find rooms in Awake, but will also try in Update if not yet initialized
            rooms = FindObjectsOfType<Room>();
        }

        void Start()
        {
            _roomCounter = 0;
            _runtimeDungeon = GetComponent<RuntimeDungeon>();
        }


        void Update()
        {
            // Check if rooms are still null or empty
            if (rooms.IsNullOrEmpty()) rooms = FindObjectsOfType<Room>();

            // If rooms are found, stop calling Update
            if (!rooms.IsNullOrEmpty())
            {
                // Disable Update by setting the enabled property to false
                enabled = false; // Disables the Update method from running again
                EventManager.EOnRoomGeneration.Invoke();
                OnDungeonGenerated(_runtimeDungeon.Generator);
                PlayerCharacter.Instance.navMeshAgent.enabled = true;
            }
        }

        void OnDungeonGenerated(DungeonGenerator generator)
        {
            var dungeonGameObject = GameObject.Find("Dungeon");
            var roomGameObjects = GetChildren(dungeonGameObject);


            foreach (var roomGameObject in roomGameObjects)
            {
                _roomCounter++;
                var roomId = _roomCounter;

                var roomComponent = roomGameObject.GetComponent<Room>();
                if (roomComponent != null)
                    roomComponent.InitializeRoom(_roomCounter); // Initialize room with the counter as ID
            }
        }


        List<GameObject> GetChildren(GameObject parent)
        {
            var children = new List<GameObject>();

            foreach (Transform child in parent.transform) children.Add(child.gameObject);

            return children;
        }
    }
}
