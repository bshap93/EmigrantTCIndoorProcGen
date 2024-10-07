using Characters.Health.Scripts.States;
using Characters.Player.Scripts;
using Core.Events.EventManagers;
using Core.GameManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health.Scripts
{
    public class OxygenBarUI : MonoBehaviour
    {
        public Image oxygenBarFill; // Reference to the UI Image for the health bar fill
        public PlayerEventManager playerEventManager;
        [SerializeField] HealthSystem healthSystem; // Reference to the HealthSystem

        void Start()
        {
            healthSystem = PlayerCharacter.Instance.GetHealthSystem(); // Get the player's health system
            if (playerEventManager == null)
                playerEventManager = GameManager.Instance.player.GetComponent<PlayerEventManager>();

            UpdateOxygenBar(healthSystem.currentOxygen);
        }

        void Update()
        {
            UpdateOxygenBar(healthSystem.currentOxygen);
        }


        public void UpdateOxygenBar(float oxygen)
        {
            var oxygenPercent = oxygen / HealthSystem.MaxOxygen;
            oxygenBarFill.fillAmount = oxygenPercent;
        }
    }
}
