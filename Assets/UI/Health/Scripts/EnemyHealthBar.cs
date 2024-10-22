using Characters.Enemies;
using Characters.Health.Scripts.States;
using Characters.Player.Scripts;
using Core.GameManager.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Health.Scripts
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Image healthBarFill; // Reference to the UI Image for the health bar fill
        public EnemyEventManager enemyEventManager;
        public Enemy enemy;
        HealthSystem _healthSystem; // Reference to the HealthSystem


        void Start()
        {
            GameManager.Instance.onSystemActivated.AddListener(OnSystemActivated);
            UnityAction<float> healthChange = UpdateHealthBar;
            enemyEventManager.AddListenerToHealthChangedEvent(healthChange);

            _healthSystem = enemy.GetHealthSystem(); // Get the player's health system


            // Initialize the health bar with the current health
            UpdateHealthBar(_healthSystem.currentSuitIntegrity);
        }


        void OnSystemActivated(string systemName)
        {
            if (systemName == "Health")
            {
                Debug.Log("Health system activated");
                _healthSystem = PlayerCharacter.Instance.GetHealthSystem(); // Get the player's health system
                // Subscribe to health change events
                UnityAction<float> healthChange = UpdateHealthBar;
                enemyEventManager.AddListenerToHealthChangedEvent(healthChange);

                // Initialize the health bar with the current health
                UpdateHealthBar(_healthSystem.currentSuitIntegrity);
            }
        }

        // Method to update the health bar fill amount
        void UpdateHealthBar(float currentHealth)
        {
            // Calculate the health percentage and update the fill amount
            var healthPercent = currentHealth / HealthSystem.MaxSuitIntegrity;
            healthBarFill.fillAmount = healthPercent;
        }
    }
}
