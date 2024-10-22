using Characters.Health.Scripts.States;
using Characters.Player.Scripts;
using Core.Events.EventManagers;
using Core.Utilities.Commands;
using UnityEngine;

namespace Environment.Interactables.Collectables.Scripts.Commands
{
    public class CollectOxygenBottleCommand : ISimpleCommand
    {
        readonly ICharacterEventManager _characterEventManager;
        readonly GameObject _collectableGameObject;
        readonly HealthSystem _healthSystem;
        public CollectOxygenBottleCommand(GameObject collectableGameObject)
        {
            _collectableGameObject = collectableGameObject;
            _characterEventManager = PlayerCharacter.Instance.GetComponent<ICharacterEventManager>();
            _healthSystem = PlayerCharacter.Instance.GetComponent<HealthSystem>();
        }
        public void Execute()
        {
            Debug.Log("CollectOxygenBottleCommand Execute");
            _healthSystem.HealOxygen(HealthSystem.MaxOxygen);
            _collectableGameObject.SetActive(false);
        }
    }
}
