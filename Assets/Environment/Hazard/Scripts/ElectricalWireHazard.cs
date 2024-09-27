﻿using System.Collections.Generic;
using Characters.Player.Scripts;
using Characters.Scripts;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class ElectricalWireHazard : MonoBehaviour, ICuttable
    {
        public ParticleSystem sparksParticleSystem;
        public GameObject scrap;


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerCharacter = other.GetComponent<PlayerCharacter>();

                if (playerCharacter == null) return;

                playerCharacter.TakeDamage(playerCharacter, 50);
            }
        }
        public void Cut(float secondsToCut)
        {
            StartCoroutine(DestroyWire(secondsToCut));
        }
        public float GetSecondsToCut()
        {
            return 0.5f;
        }

        IEnumerator<WaitForSeconds> DestroyWire(float secondsToCut)
        {
            yield return new WaitForSeconds(secondsToCut);
            sparksParticleSystem.Stop();
            scrap.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}