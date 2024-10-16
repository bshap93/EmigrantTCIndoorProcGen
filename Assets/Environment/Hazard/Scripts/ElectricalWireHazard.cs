using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using UI.Objectives.Scripts.ObjectiveTypes;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class ElectricalWireHazard : MonoBehaviour, ICuttable
    {
        public static int wireCount;
        public ParticleSystem sparksParticleSystem;
        public GameObject scrap;

        [SerializeField] float secondsToCut = 0.5f;

        [SerializeField] DestroyObjective associatedObjective;

        [SerializeField] AudioManager audioManager;

        float cutProgress;
        Coroutine cuttingCoroutine;

        void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            wireCount++;
            audioManager.OnPlayLoopingEffect.Invoke("ElectricalWireCrackle", transform.position, true);
        }


        void OnDestroy()
        {
            EventManager.EOnObjectDestroyed.Invoke(gameObject);
            wireCount--;
            if (wireCount == 0)
                audioManager.OnStopLoopingEffect.Invoke("ElectricalWireCrackle");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerCharacter = other.GetComponent<PlayerCharacter>();

                if (playerCharacter == null) return;

                playerCharacter.TakeDamage(playerCharacter, 50);
            }
        }
        public void Cut(float seconds)
        {
            cutProgress += seconds;
            if (cutProgress >= secondsToCut && cuttingCoroutine == null)
                cuttingCoroutine = StartCoroutine(DestroyWire());
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
    }
}
