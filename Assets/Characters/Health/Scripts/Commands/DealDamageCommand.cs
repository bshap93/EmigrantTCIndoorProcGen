using Characters.Enemies;
using Characters.Scripts;
using Core.Events.EventManagers;
using UnityEngine;

namespace Characters.Health.Scripts.Commands
{
    public class DealDamageCommand : IHealthSystemCommand

    {
        public void Execute(IDamageable damageable, float value, ICharacterEventManager eventManager)
        {
            var healthSystem = damageable.GetHealthSystem();
            if (damageable is Enemy)
                Debug.Log(
                    healthSystem.characterName + "'s Health drops from " + healthSystem.currentSuitIntegrity + " to " +
                    (healthSystem.currentSuitIntegrity - value));

            healthSystem.currentSuitIntegrity -= value;


            eventManager.TriggerCharacterChangeHealth(healthSystem.currentSuitIntegrity, true);
            if (healthSystem.currentSuitIntegrity <= 0)
                eventManager.TriggerCharacterDied(healthSystem.characterName);
        }
    }
}
