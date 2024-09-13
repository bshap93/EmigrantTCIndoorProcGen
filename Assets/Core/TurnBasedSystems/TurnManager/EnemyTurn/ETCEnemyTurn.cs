// EnemyTurn.cs

using System.Collections.Generic;
using Characters.NPCs.Enemies.Scripts;
using UnityEngine;

namespace Core.TurnBasedSystems.TurnManager.EnemyTurn
{
    public class ETCEnemyTurn
    {
        readonly TurnManager _turnManager;

        public ETCEnemyTurn(TurnManager manager)
        {
            _turnManager = manager;
        }

        public void TakeTurn(List<Enemy> enemies)
        {
            Debug.Log("Enemy's Turn");

            // For each enemy, take an action (move, attack, etc.)
            foreach (var enemy in enemies) enemy.TakeAction(); // Handle the AI action for each enemy

            // After all enemies act, end the enemy turn
            _turnManager.EndEnemyTurn();
        }
    }
}
