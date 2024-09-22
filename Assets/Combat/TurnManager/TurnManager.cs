using System;
using Characters.Enemies;
using Characters.Player.Scripts;
using Characters.Scripts;
using Combat.TurnManager.TurnStates;
using UnityEngine;

namespace Combat.TurnManager
{
    public class TurnManager : MonoBehaviour
    {
        public PlayerCharacter player;
        public Enemy[] enemies;
        IDamageable _currentAggressor;
        TurnState _currentState;

        public void StartCombat(IDamageable aggressor)
        {
            _currentAggressor = aggressor;
            ChangeState(new CombatStartState());
        }

        public void ChangeState(TurnState newState)
        {
            if (_currentState != null) _currentState.ExitState(this);

            _currentState = newState;
            _currentState.EnterState(this);
        }
        public void StartTurn(IDamageable character)
        {
            character.StartTurn();
        }
        public bool HasCompletedAction(IDamageable character)
        {
            throw new NotImplementedException();
        }

        public IDamageable GetCurrentAggressor()
        {
            return _currentAggressor;
        }
    }
}
