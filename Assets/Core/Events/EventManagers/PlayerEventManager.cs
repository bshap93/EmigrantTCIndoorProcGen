﻿using Characters.Health.Scripts.States;
using Environment.Interactables.Scripts;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Events.EventManagers
{
    public class PlayerEventManager : MonoBehaviour, ICharacterEventManager
    {
        // Health has changed for a character by a certain amount
        public UnityEvent<float, bool> playerHealthChangedEvent = new();
        // Oxygen has changed for a character by a certain amount
        public UnityEvent<float, bool> playerOxygenChangedEvent = new();
        public UnityEvent playerStateInitializedEvent = new();

        public UnityEvent<string> playerDiedEvent = new();
        public UnityEvent<IDamageable, float> playerTakesDamageEvent = new();
        public UnityEvent<HealthSystem.SuitModificationType> playerSuitModEvent = new();
        public UnityEvent<InteractableObject> playerInteractedEvent = new();
        public UnityEvent<InteractableObject> playerEndedInteractionEvent = new();
        public UnityEvent<HealthSystem.SuitModificationType> playerUsedSuitModificationToolEvent = new();

        public void TriggerCharacterTakesDamage(IDamageable damageable, float damage)
        {
            playerTakesDamageEvent.Invoke(damageable, damage);
        }
        public void TriggerCharacterStateInitialized()
        {
            playerStateInitializedEvent.Invoke();
        }
        public void AddListenerToCharacterEvent(UnityAction listener)
        {
            playerStateInitializedEvent.AddListener(listener);
        }

        // Optionally expose RemoveListener for cleanup
        public void RemoveListenerFromCharacterEvent(UnityAction listener)
        {
            playerStateInitializedEvent.RemoveListener(listener);
        }

        public void AddListenerToHealthChangedEvent(UnityAction<float, bool> listener)
        {
            playerHealthChangedEvent.AddListener(listener);
        }
        public void AddListenerToCharacterEvent(UnityAction<string> listener)
        {
            playerDiedEvent.AddListener(listener);
        }
        public void RemoveListenerFromCharacterEvent(UnityAction<string> listener)
        {
            playerDiedEvent.RemoveListener(listener);
        }
        public void AddListenerToOxygenChangedEvent(UnityAction<float, bool> oxygenChange)
        {
            playerOxygenChangedEvent.AddListener(oxygenChange);
        }
        public void AddListenerToSuitModificationEvent(UnityAction<HealthSystem.SuitModificationType> suitModType)
        {
            playerSuitModEvent.AddListener(suitModType);
        }
        public void RemoveListenerFromOxygenChangedEvent(UnityAction<float, bool> oxygenChange)
        {
            playerOxygenChangedEvent.RemoveListener(oxygenChange);
        }

        public void TriggerCharacterChangeHealth(float health, bool damage)
        {
            playerHealthChangedEvent.Invoke(health, damage);
        }
        public void TriggerCharacterChangeOxygen(float oxygen, bool isRestored)
        {
            playerOxygenChangedEvent.Invoke(oxygen, isRestored);
        }

        public void TriggerCharacterDied(string characterName)
        {
            playerDiedEvent.Invoke(characterName);
        }

        public void TriggerCharacterSuitModification(HealthSystem.SuitModificationType suitModType)
        {
            playerSuitModEvent.Invoke(suitModType);
            if (suitModType == HealthSystem.SuitModificationType.FullRepair)
                EventManager.EOnItemUsed.Invoke("9198590967925931573");

            Debug.Log("Suit Modification Event Invoked");
        }


        public void TriggerCharacterUsedSuitModificationTool(HealthSystem.SuitModificationType suitModificationType)
        {
            playerUsedSuitModificationToolEvent.Invoke(suitModificationType);
        }

        public void AddListenerToSuitModificationToolEvent(UnityAction<HealthSystem.SuitModificationType> listener)
        {
            playerUsedSuitModificationToolEvent.AddListener(listener);
        }

        public void RemoveListenerFromSuitModificationToolEvent(UnityAction<HealthSystem.SuitModificationType> listener)
        {
            playerUsedSuitModificationToolEvent.RemoveListener(listener);
        }

        // Encapsulate AddListener logic

        public void TriggerPlayerInteracted(InteractableObject interactableObject)
        {
            playerInteractedEvent.Invoke(interactableObject);
        }

        public void TriggerPlayerEndedInteraction(InteractableObject interactableObject)
        {
            playerEndedInteractionEvent.Invoke(interactableObject);
        }

        public void AddListenerToPlayerInteractedEvent(UnityAction<InteractableObject> listener)
        {
            playerInteractedEvent.AddListener(listener);
        }

        public void AddListenerToPlayerEndedInteractionEvent(UnityAction<InteractableObject> listener)
        {
            playerEndedInteractionEvent.AddListener(listener);
        }

        public void RemoveListenerFromPlayerEndedInteractionEvent(UnityAction<InteractableObject> listener)
        {
            playerEndedInteractionEvent.RemoveListener(listener);
        }

        public void RemoveListenerFromPlayerInteractedEvent(UnityAction<InteractableObject> listener)
        {
            playerInteractedEvent.RemoveListener(listener);
        }

        public void RemoveListenerFromOxygenChange(UnityAction<float, bool> listener)
        {
            playerOxygenChangedEvent.RemoveListener(listener);
        }

        // Optionally expose RemoveListener for cleanup
        public void RemoveListenerFromSuitIntegrityChange(UnityAction<float, bool> listener)
        {
            playerHealthChangedEvent.RemoveListener(listener);
        }

        public void AddListenerToPlayerTakesDamageEvent(UnityAction<IDamageable, float> listener)
        {
            playerTakesDamageEvent.AddListener(listener);
        }

        public void RemoveListenerFromPlayerTakesDamageEvent(UnityAction<IDamageable, float> listener)

        {
            playerTakesDamageEvent.RemoveListener(listener);
        }
    }
}
