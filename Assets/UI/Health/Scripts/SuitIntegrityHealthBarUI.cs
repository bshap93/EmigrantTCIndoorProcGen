using Characters.Health.Scripts.States;
using Characters.Player.Scripts;
using Core.Events.EventManagers;
using Core.GameManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health.Scripts
{
    public class SuitIntegrityHealthBarUI : MonoBehaviour
    {
        public Image suitIntegrityBarFill; // Reference to the UI Image for the health bar fill
        public PlayerEventManager playerEventManager;
        [SerializeField] HealthSystem healthSystem; // Reference to the HealthSystem

        void Start()
        {
            healthSystem = PlayerCharacter.Instance.GetHealthSystem(); // Get the player's health system
            if (playerEventManager == null)
                playerEventManager = GameManager.Instance.player.GetComponent<PlayerEventManager>();

            UpdateSuitIntegrityBar(healthSystem.currentSuitIntegrity);
        }

        void Update()
        {
            UpdateSuitIntegrityBar(healthSystem.currentSuitIntegrity);
        }

        public void UpdateSuitIntegrityBar(float suitIntegrity)
        {
            var suitIntegrityPercent = suitIntegrity / HealthSystem.MaxSuitIntegrity;
            suitIntegrityBarFill.fillAmount = suitIntegrityPercent;
        }
    }
}
