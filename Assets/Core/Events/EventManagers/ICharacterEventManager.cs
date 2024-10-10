using Characters.Health.Scripts.States;
using Characters.Scripts;
using UnityEngine.Events;

namespace Core.Events.EventManagers
{
    public interface ICharacterEventManager
    {
        /// <summary>
        ///     Invokers
        /// </summary>
        /// <param name="health"></param>
        public void TriggerCharacterChangeHealth(float health, bool damage);

        public void TriggerCharacterChangeOxygen(float oxygen, bool isRestored);

        public void TriggerCharacterDied(string characterName);

        public void TriggerCharacterTakesDamage(IDamageable damageable, float damage);

        public void TriggerCharacterStateInitialized();

        public void TriggerCharacterSuitModification(HealthSystem.SuitModificationType suitModificationType);


        /// <summary>
        ///     Listeners
        /// </summary>
        /// <param name="listener"></param>
        public void AddListenerToCharacterEvent(UnityAction listener);

        public void RemoveListenerFromCharacterEvent(UnityAction listener);

        public void AddListenerToHealthChangedEvent(UnityAction<float, bool> listener);


        public void AddListenerToCharacterEvent(UnityAction<string> listener);

        public void RemoveListenerFromCharacterEvent(UnityAction<string> listener);
        public void AddListenerToOxygenChangedEvent(UnityAction<float, bool> oxygenChange);
        public void RemoveListenerFromOxygenChangedEvent(UnityAction<float, bool> oxygenChange);


        public void AddListenerToSuitModificationEvent(UnityAction<HealthSystem.SuitModificationType> suitRepair);
    }
}
