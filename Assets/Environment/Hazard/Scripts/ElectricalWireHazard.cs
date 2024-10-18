using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using Core.GameManager.Scripts;
using UI.Objectives.Scripts.ObjectiveTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment.Hazard.Scripts
{
    public class ElectricalWireHazard : MonoBehaviour, ICuttable
    {
        static readonly Dictionary<string, int> WireCountPerScene = new();
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

        void Start()
        {
            if (floorManager.isElectricictyOn == false) liveWire = false;

            audioManager = AudioManager.Instance;
            currentSceneName = SceneManager.GetActiveScene().name;


            if (!WireCountPerScene.ContainsKey(currentSceneName)) WireCountPerScene[currentSceneName] = 0;
            WireCountPerScene[currentSceneName]++;

            if (liveWire)
                audioManager.OnPlayLoopingEffect.Invoke("ElectricalWireCrackle", transform.position, true);
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
                if (WireCountPerScene[currentSceneName] == 0 && liveWire)
                    audioManager.OnStopLoopingEffect.Invoke("ElectricalWireCrackle");
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
