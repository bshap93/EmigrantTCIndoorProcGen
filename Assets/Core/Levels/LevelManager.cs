using System.Collections.Generic;
using DunGen;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Core.Levels
{
    public class LevelManager : MonoBehaviour
    {
        // UnityEvent for when the level is generated (does not require the DungeonGenerator parameter)
        public UnityEvent onLevelGenerated;

        public RuntimeDungeon runtimeDungeon;
        public int currentLevelID;

        [SerializeField] int dungeonSeed;

        DungeonGenerator _dungeonGenerator; // Reference to DunGen's DungeonGenerator

        public List<DungeonLevel> DungeonLevels;
        public static LevelManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DungeonLevels = new List<DungeonLevel>();
            if (runtimeDungeon != null) _dungeonGenerator = runtimeDungeon.Generator;
        }

        public void SetSeed(int seed)
        {
            dungeonSeed = seed;
        }

        public int GetSeed()
        {
            return dungeonSeed;
        }










        // Called when DunGen completes the dungeon generation
        void HandleDungeonGenerated(DungeonGenerator generator)
        {
            // Unsubscribe to avoid multiple triggers
            _dungeonGenerator.OnGenerationComplete -= HandleDungeonGenerated;

            // Invoke the custom UnityEvent for other listeners
            onLevelGenerated?.Invoke();

        }
    }
}
