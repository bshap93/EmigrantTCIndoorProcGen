using Characters.Player.Scripts.States;
using UnityEngine;

namespace Characters.Player.Scripts
{
    public class PlayerStateController : MonoBehaviour
    {
        PlayerState _currentState;
        PlayerCharacter _playerCharacter;

        public void Update()
        {
            if (_currentState != null) _currentState.Update(_playerCharacter);
        }

        public void Initialize(PlayerCharacter playerCharacter, PlayerState initialState)
        {
            _playerCharacter = playerCharacter;
            _currentState = initialState;
            _currentState.Enter(_playerCharacter); // Set the initial state
        }

        public void ChangeState(PlayerState newState)
        {
            if (_currentState.GetType() != newState.GetType())
            {
                if (_currentState != null)
                    _currentState.Exit(_playerCharacter);

                _currentState = newState;
                _currentState.Enter(_playerCharacter);
            }
        }
        public PlayerState GetCurrentState()
        {
            return _currentState;
        }
    }
}
