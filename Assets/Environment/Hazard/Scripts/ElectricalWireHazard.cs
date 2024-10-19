using System;
using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using UI.Objectives.Scripts.ObjectiveTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Environment.Hazard.Scripts
{
    public class ElectricalWireHazard : MonoBehaviour, ICuttable
    {
        static readonly Dictionary<string, int> WireCountPerScene = new();
        static AudioManager _sharedAudioManager;
        // Static reference to the AudioManager
        [FormerlySerializedAs("BeaconParticleSystem")]
        public ParticleSystem beaconParticleSystem;
        public ParticleSystem sparksParticleSystem;
        public GameObject scrap;

        [SerializeField] float secondsToCut = 0.5f;
        [SerializeField] DestroyObjective associatedObjective;
        [SerializeField] AudioManager audioManager;
        [SerializeField] bool liveWire = true;
        [SerializeField] FloorManager floorManager;

        float _cutProgress;
        Coroutine _cuttingCoroutine;
        string currentSceneName;
        static ElectricalWireHazard()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void Awake()
        {
            // If this is the first ElectricalWireHazard to be created, find the AudioManager
            if (_sharedAudioManager == null) _sharedAudioManager = FindObjectOfType<AudioManager>();
        }

        void Start()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            if (!WireCountPerScene.ContainsKey(currentSceneName)) WireCountPerScene[currentSceneName] = 0;
            WireCountPerScene[currentSceneName]++;

            if (liveWire && _sharedAudioManager != null)
            {
                _sharedAudioManager.OnPlayLoopingEffect.Invoke("ElectricalWireCrackle", transform.position, true);
            }
            else
            {
                sparksParticleSystem.Stop();
                beaconParticleSystem.Stop();
                Debug.Log("Wire is not live");
            }
        }

        void OnEnable()
        {
        }

        void OnDestroy()
        {
            EventManager.EOnObjectDestroyed.Invoke(gameObject);

            if (WireCountPerScene.ContainsKey(currentSceneName))
            {
                WireCountPerScene[currentSceneName]--;
                if (WireCountPerScene[currentSceneName] == 0 && liveWire && _sharedAudioManager != null)
                    _sharedAudioManager.OnStopLoopingEffect.Invoke("ElectricalWireCrackle");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && liveWire)
            {
                var playerCharacter = other.GetComponent<PlayerCharacter>();

                if (playerCharacter == null) return;

                playerCharacter.TakeDamage(playerCharacter, 50);
            }
        }


        public void Cut(float seconds)
        {
            _cutProgress += seconds;
            if (_cutProgress >= secondsToCut && _cuttingCoroutine == null)
                _cuttingCoroutine = StartCoroutine(DestroyWire());
        }

        public float GetSecondsToCut()
        {
            return secondsToCut;
        }
        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Find all ElectricalWireHazard objects in the new scene and set their AudioManager
            var wires = FindObjectsOfType<ElectricalWireHazard>();
            foreach (var wire in wires) wire.SetAudioManager(_sharedAudioManager);
        }
        void SetAudioManager(object sharedAudioManager)
        {
            throw new NotImplementedException();
        }

        // Method to set the AudioManager (called when a new scene is loaded)
        public void SetAudioManager(AudioManager audioManager)
        {
            _sharedAudioManager = audioManager;
        }

        IEnumerator<WaitForSeconds> DestroyWire()
        {
            yield return new WaitForSeconds(0.1f); // Small delay to ensure we don't destroy immediately
            sparksParticleSystem.Stop();
            scrap.SetActive(true);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        // Static method to get wire count for the current scene
        public static int GetWireCountForCurrentScene()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            return WireCountPerScene.ContainsKey(sceneName) ? WireCountPerScene[sceneName] : 0;
        }
    }
}
