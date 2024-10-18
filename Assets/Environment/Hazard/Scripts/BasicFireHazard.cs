using System;
using System.Collections.Generic;
using Audio.Sounds.Scripts;
using Characters.Player.Scripts;
using Characters.Scripts;
using Core.Events;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class BasicFireHazard : MonoBehaviour, IExtinguishable
    {
        [SerializeField] float secondsToExtinguish = 0.5f;
        [SerializeField] AudioManager audioManager;
        [SerializeField] string audioClipName = "FireBlazing";


        void Start()
        {
            AudioManager.Instance.PlayLoopingEffect(audioClipName, transform.position, true);
            EventManager.EOnObjectDestroyed.AddListener(OnThisFireExtinguished);
        }

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


        public void Extinguish(float sToExtinguish)
        {
            StartCoroutine(ExtinguishFire());
        }
        public float GetSecondsToExtinguish()
        {
            return secondsToExtinguish;
        }
        void OnThisFireExtinguished(GameObject arg0)
        {
            throw new NotImplementedException();
        }
        IEnumerator<WaitForSeconds> ExtinguishFire()
        {
            yield return new WaitForSeconds(secondsToExtinguish);
            Destroy(gameObject);
        }
    }
}
