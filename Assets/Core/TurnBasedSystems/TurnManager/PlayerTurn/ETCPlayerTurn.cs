using UnityEngine;

namespace Core.TurnBasedSystems.TurnManager.PlayerTurn
{
    public class ETCPlayerTurn
    {
        readonly TurnManager _turnManager;

        public ETCPlayerTurn(TurnManager manager)
        {
            _turnManager = manager;
        }

        public void TakeTurn()
        {
            // Here, you can wait for the player input (move, attack, etc.)
            // Once the player completes their action, end their turn
            Debug.Log("Player's Turn");

            // Example: End turn after an action
            _turnManager.EndPlayerTurn();
        }
    }
}
