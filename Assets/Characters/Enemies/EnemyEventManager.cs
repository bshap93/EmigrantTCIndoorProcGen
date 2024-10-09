using System;
using Characters.Health.Scripts.States;
using Characters.Scripts;
using Core.Events.EventManagers;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemies
{
    public class EnemyEventManager : MonoBehaviour, ICharacterEventManager
    {
        // Health has changed for a character by a certain amount
        public UnityEvent<float> enemyChangeHealthEvent = new();

        public UnityEvent enemyStateInitializedEvent = new();

        public UnityEvent<string> enemyDiedEvent = new();

        public UnityEvent<IDamageable, float> enemyTakesDamageEvent = new();
        public void TriggerCharacterChangeOxygen(float oxygen, bool isRestored)
        {
            throw new NotImplementedException();
        }
        public void TriggerCharacterDied(string characterName)
        {
            enemyDiedEvent.Invoke(characterName);
        }
        public void TriggerCharacterTakesDamage(IDamageable damageable, float damage)
        {
            enemyTakesDamageEvent.Invoke(damageable, damage);
        }
        public void TriggerCharacterStateInitialized()
        {
            enemyStateInitializedEvent.Invoke();
        }
        public void TriggerCharacterSuitRepair(HealthSystem.SuitModificationType suitModificationType)
        {
            throw new NotImplementedException();
        }
        public void AddListenerToCharacterEvent(UnityAction listener)
        {
            enemyStateInitializedEvent.AddListener(listener);
        }
        public void RemoveListenerFromCharacterEvent(UnityAction listener)
        {
            enemyStateInitializedEvent.RemoveListener(listener);
        }
        public void AddListenerToCharacterEvent(UnityAction<string> listener)
        {
            enemyDiedEvent.AddListener(listener);
        }
        public void RemoveListenerFromCharacterEvent(UnityAction<string> listener)
        {
            enemyDiedEvent.RemoveListener(listener);
        }
        public void AddListenerToOxygenChangedEvent(UnityAction<float, bool> oxygenChange)
        {
            throw new NotImplementedException();
        }
        public void RemoveListenerFromOxygenChangedEvent(UnityAction<float, bool> oxygenChange)
        {
            throw new NotImplementedException();
        }
        public void AddListenerToSuitRepairEvent(UnityAction<HealthSystem.SuitModificationType> suitRepair)
        {
            throw new NotImplementedException();
        }
        public void TriggerCharacterChangeHealth(float health, bool damage)
        {
            throw new NotImplementedException();
        }
        public void AddListenerToHealthChangedEvent(UnityAction<float, bool> listener)
        {
            throw new NotImplementedException();
        }
        public void TriggerCharacterChangeOxygen(float oxygen)
        {
            throw new NotImplementedException();
        }
        public void AddListenerToOxygenChangedEvent(UnityAction<float> oxygenChange)
        {
            throw new NotImplementedException();
        }
        public void RemoveListenerFromOxygenChangedEvent(UnityAction<float> oxygenChange)
        {
            throw new NotImplementedException();
        }
        public void TriggerCharacterChangeHealth(float health)
        {
            enemyChangeHealthEvent.Invoke(health);
        }
        public void AddListenerToHealthChangedEvent(UnityAction<float> listener)
        {
            enemyChangeHealthEvent.AddListener(listener);
        }
        public void RemoveListenerFromCharacterEvent(UnityAction<float> listener)
        {
            enemyChangeHealthEvent.RemoveListener(listener);
        }
    }
}
