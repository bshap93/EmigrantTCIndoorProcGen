using Characters.Player.Scripts;
using UnityEngine;

namespace Characters.Health.Scripts.States.OxygenState
{
    public class OxygenLeakingState : IOxygenState
    {
        readonly HealthSystem _healthSystem;
        readonly PlayerCharacter _playerCharacter;

        public OxygenLeakingState(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            _playerCharacter = PlayerCharacter.Instance;
        }
        public void Update()
        {
            _healthSystem.currentOxygen -= Time.deltaTime * _healthSystem.oxygenDepletionRate;
            _playerCharacter.playerEventManager.TriggerCharacterChangeOxygen(_healthSystem.currentOxygen, false);
        }
        public void Enter()
        {
            _healthSystem.inGameConsoleManager.LogMessage("Oxygen is leaking!");
        }
        public void Exit()
        {
            _healthSystem.inGameConsoleManager.LogMessage("Oxygen leak fixed!");
        }

        public HealthSystem.OxygenState GetState()
        {
            return HealthSystem.OxygenState.Leaking;
        }
    }
}
