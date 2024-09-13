using System.Collections.Generic;
using System.Linq;
using Characters.NPCs.Enemies.Scripts;
using Core.TurnBasedSystems.TurnManager.EnemyTurn;
using Core.TurnBasedSystems.TurnManager.PlayerTurn;
using UnityEngine;

namespace Core.TurnBasedSystems.TurnManager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] List<Enemy> enemies;
        ETCEnemyTurn _etcEnemyTurn;

        ETCPlayerTurn _etcPlayerTurn;

        public static TurnManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep the manager across scenes
            }
            else
            {
                Destroy(gameObject); // Prevent duplicates
            }
        }

        void Start()
        {
            _etcPlayerTurn = new ETCPlayerTurn(this);
            _etcEnemyTurn = new ETCEnemyTurn(this);
            enemies = FindObjectsOfType<Enemy>().ToList();

            // Start the player's turn first
            StartPlayerTurn();
        }


        public void StartPlayerTurn()
        {
            _etcPlayerTurn.TakeTurn();
        }

        public void EndPlayerTurn()
        {
            StartEnemyTurn();
        }

        public void StartEnemyTurn()
        {
            _etcEnemyTurn.TakeTurn(enemies);
        }

        public void EndEnemyTurn()
        {
            StartPlayerTurn();
        }
    }
}
