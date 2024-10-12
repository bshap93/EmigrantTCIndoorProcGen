using System.Collections.Generic;
using Characters.Player.Scripts;
using Characters.Scripts;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class BasicFireHazard : MonoBehaviour, IExtinguishable
    {
        [SerializeField] float secondsToExtinguish = 0.5f;
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
        IEnumerator<WaitForSeconds> ExtinguishFire()
        {
            yield return new WaitForSeconds(secondsToExtinguish);
            Destroy(gameObject);
        }
    }
}
