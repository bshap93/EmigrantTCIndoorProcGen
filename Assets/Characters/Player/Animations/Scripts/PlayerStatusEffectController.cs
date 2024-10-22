using System.Collections.Generic;
using Characters.Health.Scripts.States;
using Core.Events.EventManagers;
using UnityEngine;

namespace Characters.Player.Animations.Scripts
{
    public class PlayerStatusEffectController : MonoBehaviour
    {
        [SerializeField] ParticleSystem restoreSuitEffect;

        PlayerEventManager _playerEventManager;

        Dictionary<HealthSystem.SuitModificationType, ParticleSystem> _suitModificationEffects;
        public PlayerStatusEffectController(
            Dictionary<HealthSystem.SuitModificationType, ParticleSystem> suitModificationEffects)
        {
            _suitModificationEffects = suitModificationEffects;
        }

        void Start()
        {
            _playerEventManager = FindObjectOfType<PlayerEventManager>();
            _suitModificationEffects = new Dictionary<HealthSystem.SuitModificationType, ParticleSystem>();
            _suitModificationEffects.Add(HealthSystem.SuitModificationType.FullRepair, restoreSuitEffect);
            _playerEventManager.AddListenerToSuitModificationEvent(PlayModifySuitEffect);
        }

        void PlayModifySuitEffect(HealthSystem.SuitModificationType suitModificationType)
        {
            if (_suitModificationEffects.ContainsKey(suitModificationType))
                _suitModificationEffects[suitModificationType].Play();
        }
    }
}
