using System.Collections.Generic;
using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using UI.Objectives.Scripts.ObjectiveTypes;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class ElectricalWireHazard : MonoBehaviour, ICuttable
    {
        public ParticleSystem sparksParticleSystem;
        public GameObject scrap;

        [SerializeField] float secondsToCut = 0.5f;

        [SerializeField] DestroyObjective associatedObjective;

        void OnDestroy()
        {
            EventManager.EOnObjectDestroyed.Invoke(gameObject);
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
            StartCoroutine(DestroyWire(secondsToCut));
        }
        public float GetSecondsToCut()
        {
            return secondsToCut;
        }

        IEnumerator<WaitForSeconds> DestroyWire(float secondsToCut)
        {
            yield return new WaitForSeconds(secondsToCut);
            sparksParticleSystem.Stop();
            scrap.SetActive(true);
            gameObject.SetActive(false);


            Destroy(gameObject);
        }
    }
}
