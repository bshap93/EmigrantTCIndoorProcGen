using Characters.Player.Scripts;
using UnityEngine;

namespace Environment.Hazard.Scripts
{
    public class ElectricalHazardBase : MonoBehaviour
    {
        public float damageDealt = 50;

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerCharacter = other.GetComponent<PlayerCharacter>();

                if (playerCharacter == null) return;

                playerCharacter.TakeDamage(playerCharacter, damageDealt);
            }
        } 
    }
}
