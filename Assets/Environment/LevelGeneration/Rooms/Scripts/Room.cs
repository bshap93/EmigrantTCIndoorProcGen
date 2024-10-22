using System;
using System.Collections.Generic;
using Core.Events;
using Core.Spawning.Scripts;
using DunGen;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.LevelGeneration.Rooms.Scripts
{
    public class Room : MonoBehaviour
    {
        public List<SpawnPoint25D> enemySpawnPoints = new(); // Define where enemies can spawn in this room
        public List<GameObject> enemyPrefabs = new(); // Different enemy types that can be spawned
        [SerializeField] int baseEnemyCount; // Number of enemies to spawn in this room
        [SerializeField] bool hasVerticality; // If the room has verticality, enemies can spawn on different floors

        [SerializeField] Transform roomCenter;
        [SerializeField] int difficultyLevel;

        [SerializeField] RoomType roomType;

        [SerializeField] int roomID;

        [SerializeField] Tile roomTile; // The tile that represents this room

        // Room bounds (could be defined by the room size or manually assigned)

        public LayerMask obstacleLayer; // For checking spawn point validity
        Tile _roomTile;

        void Start()
        {
            _roomTile = GetComponent<Tile>();
            EventManager.EPlayerEnteredRoom.AddListener(OnPlayerEnterRoom);
        }

        public void InitializeRoom(int id)
        {
            roomID = id;
        }


        void OnPlayerEnterRoom()
        {
            SpawnEnemies(difficultyLevel);
        }
        void SpawnEnemies(int difficultyNumAddEnemies)
        {
            if (enemySpawnPoints.Count == 0) return;
            if (enemyPrefabs.Count == 0) return;

            var enemyCount = Mathf.Min(
                enemySpawnPoints.Count,
                baseEnemyCount + difficultyNumAddEnemies); // Scale enemy count based on difficulty

            for (var i = 0; i < enemyCount; i++)
            {
                var spawnPoint = enemySpawnPoints[i];
                if (spawnPoint.CanSpawn())
                {
                    var enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                    Instantiate(enemyPrefab, spawnPoint.GetSpawnPosition(), Quaternion.identity);
                    Debug.Log("Spawned enemy");
                    spawnPoint.MarkOccupied();
                }
            }
        }


        // A simple script to define a room, attach this to each room
        public Transform GetRoomCenter()
        {
            return roomCenter;
        }
        public int GetRoomId()
        {
            return roomID;
        }

        [Serializable]
        enum RoomType
        {
            CapRoom,
            Corridor,
            Room3X3,
            Other
        }
    }
}
