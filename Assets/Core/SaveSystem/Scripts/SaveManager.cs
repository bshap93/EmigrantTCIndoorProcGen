using System.Collections.Generic;
using Characters.Player.Scripts;
using Core.Levels;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.SaveSystem.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        [FormerlySerializedAs("player")] [FormerlySerializedAs("playerStateController")]
        public PlayerCharacter playerCharacter; // Drag the PlayerCharacter object here
        Transform _initialCameraTransform; // Drag the Camera object here
        void Start()
        {
            if (PlayerCharacter.Instance == null)
                Debug.LogError("PlayerCharacter is not initialized!");

            if (LevelManager.Instance == null)
                Debug.LogError("LevelManager is not initialized!");

            if (Camera.main != null) _initialCameraTransform = Camera.main.transform;
        }

        [Button("Save Game")]
        public void SaveGame()
        {
            if (playerCharacter == null || LevelManager.Instance == null)
            {
                Debug.LogError(
                    "Missing references in SaveManager. Ensure PlayerCharacter and LevelManager are assigned.");

                return;
            }

            // Simple test save
            ES3.Save("playerPosition", playerCharacter.transform.position);
            ES3.Save("cameraTransform", _initialCameraTransform);
            ES3.Save("dungeonSeed", LevelManager.Instance.GetSeed());
            SaveDungeonLevels();
        }

        public void InitializedDungeonLevel(int? seed)
        {
            LevelManager.Instance.GenerateLevel(seed);

            SaveGame();
        }


        void SaveDungeonLevels()
        {
            // Save the list of DungeonLevel objects
            ES3.Save("DungeonLevels", LevelManager.Instance.DungeonLevels);
        }

        void LoadDungeonLevels()
        {
            // Load the list of DungeonLevel objects
            if (ES3.KeyExists("DungeonLevels"))
                LevelManager.Instance.DungeonLevels = ES3.Load<List<DungeonLevel>>("DungeonLevels");
            else
                Debug.LogWarning("No saved data found!");
        }


        [Button("Load Game")]
        public void LoadGame()
        {
            if (!ES3.KeyExists("playerPosition") || !ES3.KeyExists("dungeonSeed") || !ES3.KeyExists("DungeonLevels"))
            {
                Debug.LogWarning("No saved data found!");
                return;
            }

            playerCharacter.transform.position = ES3.Load<Vector3>("playerPosition");
            LevelManager.Instance.GenerateLevel(ES3.Load<int>("dungeonSeed"));
            _initialCameraTransform.position = ES3.Load<Vector3>("cameraTransform");
            LoadDungeonLevels();


            Debug.Log("Game loaded successfully!");
        }

        [Button("Delete Save")]
        public void DeleteSave()
        {
            if (!ES3.KeyExists("playerPosition") || !ES3.KeyExists("dungeonSeed") || !ES3.KeyExists("DungeonLevels"))
            {
                Debug.LogWarning("Incomplete or missing save data");
                return;
            }

            ES3.DeleteKey("playerPosition");
            ES3.DeleteKey("dungeonSeed");
            ES3.DeleteKey("DungeonLevels");
            ES3.DeleteKey("cameraTransform");
        }

        [Button("Delete File")]
        public void DeleteSaveFile()
        {
            ES3.DeleteFile();
        }


        [Button("List Saved Levels Dungeons")]
        public void ListLevelsInSave()
        {
            if (ES3.KeyExists("DungeonLevels"))
            {
                var dungeonLevels = ES3.Load<List<DungeonLevel>>("DungeonLevels");

                Debug.LogWarning("Hi");

                foreach (var level in dungeonLevels) Debug.Log($"Level ID: {level.LevelID}, Seed: {level.Seed}");
            }
            else
            {
                Debug.LogWarning("No saved data found!");
            }
        }
    }
}
