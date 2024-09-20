using System.Collections.Generic;
using Characters.Enemies.Attacks.Commands;
using Characters.Enemies.Scripts;
using Characters.Enemies.States;
using Characters.Health.Scripts;
using Characters.Health.Scripts.Commands;
using Characters.Player.Scripts;
using Characters.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies
{
    /// <summary>
    ///     The Enemy class is responsible for managing the enemy's states
    ///     as well as movement
    /// </summary>
    public class Enemy : MonoBehaviour, IDamageable
    {
        public enum AttackType
        {
            Melee,
            Ranged
        }


        static int _enemyCount;
        public AttackType attackType;
        public NavMeshAgent navMeshAgent;
        public List<Transform> waypoints;
        public Transform player;
        public Animator enemyAnimator;


        public float attackCooldown = 1.5f; // Cooldown duration in second


        [SerializeField] float detectionRange = 10f;
        [SerializeField] float attackRange = 2f;
        [SerializeField] float staggerTime = 5f;

        [SerializeField] float memoryDuration = 5f; // How long the enemy remembers the player after losing sight

        public GameObject weapon;
        public double chaseDuration;
        [SerializeField] EnemyEventManager enemyEventManager;
        readonly List<DOTweenAnimation> _animations = new();
        readonly List<IAttackCommand> _attacks = new();

        EnemyAttack _enemyAttack;
        string _enemyName;
        HealthSystem _healthSystem;

        bool _isDead;
        float _memoryTimer;
        EnemyNavigation _navigation;
        EnemyStateController _stateController;
        EnemyVisiblity _visibility;

        // Start is called before the first frame update
        void Start()
        {
            _enemyCount++;
            _enemyName = "Enemy" + _enemyCount;

            if (enemyEventManager == null)
                // Enemy event manager should be attached to same object
                // as the enemy script
                enemyEventManager = GetComponent<EnemyEventManager>();

            _navigation = GetComponent<EnemyNavigation>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _stateController = GetComponent<EnemyStateController>();


            _healthSystem = new HealthSystem(_enemyName, 100, enemyEventManager);
            // Get the EnemyVisiblity component
            _visibility = GetComponent<EnemyVisiblity>();
            // Set the initial state to patrolling, and former state to null
            if (_enemyCount % 2 == 0)
                _stateController.Initialize(this, new PatrollingState(null, true));
            else
                _stateController.Initialize(this, new PatrollingState(null, false));


            // Find the player object
            if (player == null)
                player = GameObject.FindWithTag("Player").transform;

            // Get the NavMeshAgent component
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            _stateController.Update();
        }
        public void TakeDamage(IDamageable dmgeable, float damage)
        {
            if (dmgeable is Enemy)
            {
                var dealDamageCommand = new DealDamageCommand();
                dealDamageCommand.Execute(dmgeable, damage, enemyEventManager);
                var healthSystem = dmgeable.GetHealthSystem();
                if (healthSystem.CurrentHealth <= 0)
                {
                    ChangeState(new DeadState(enemyAnimator, _stateController.GetCurrentState()));
                    _isDead = true;
                }
                else
                {
                    ChangeState(
                        new StaggeredState(
                            enemyAnimator,
                            _stateController.GetCurrentState(), staggerTime, null));
                }
            }
        }
        public HealthSystem GetHealthSystem()
        {
            if (_healthSystem == null)
                _healthSystem = new HealthSystem(_enemyName, 100, enemyEventManager);

            return _healthSystem;
        }
        public void FindWaypoints()
        {
            var waypointParent = GameObject.Find("Waypoints");

            if (waypointParent != null)
            {
                foreach (Transform child in waypointParent.transform)
                    waypoints.Add(child);

                Debug.Log("Enemy Found " + waypoints.Count + " waypoints");
            }
        }


        public void SetEnemyDestination(Vector3 destination)
        {
            _navigation.SetDestination(destination);
        }

        public bool HasEnemyReachedDestination()
        {
            return _navigation.HasReachedDestination();
        }

        public void ChangeState(EnemyState newState)
        {
            if (!_isDead)
                _stateController.ChangeState(newState);
        }

        public bool CanSeePlayer()
        {
            if (_visibility.TargetIsVisible)
            {
                _memoryTimer = Time.time + memoryDuration; // Reset memory timer when the player is visible
                return true;
            }

            // If memory timer is still running, keep chasing
            if (Time.time < _memoryTimer) return true; // The enemy is still chasing even if they lost sight

            return false;
        }

        /// <summary>
        ///     Without considering ability to see the player, check
        ///     if the player is within a certain range of the enemy
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        bool IsPlayerInRange(float range)
        {
            return Vector3.Distance(transform.position, player.position) < range;
        }

        // Call for specific ranges

        public bool IsPlayerInAttackRange()
        {
            return IsPlayerInRange(attackRange);
        }

        public bool IsPlayerInChaseRange()
        {
            return IsPlayerInRange(detectionRange);
        }

        // Control the enemy's movement in NavMesh 

        public void StopMoving()
        {
            _navigation.StopMoving();
        }
        public void StartMoving()
        {
            _navigation.StartMoving();
        }


        public void PerformAttack(IAttackCommand attackCommand)
        {
            _enemyAttack.PerformAttack(
                player.gameObject.GetComponent<PlayerCharacter>());
        }
        public Vector3 GetPlayerPosition()
        {
            return player.position;
        }
        public IAttackCommand GetAttack()
        {
            return _enemyAttack.GetAttack();
        }
    }
}
