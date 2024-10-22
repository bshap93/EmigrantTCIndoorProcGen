using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.States
{
    public class ChaseState : EnemyState
    {
        readonly float _pathRecalculationDelay = 0.5f; // Half a second delay between recalculations
        readonly bool _patrolWasReversed;
        readonly Transform _target;
        float _chaseTimer;
        float _pathRecalculationTimer;
        public ChaseState(EnemyState formerState, Transform target) : base(formerState, target)
        {
            _target = target;

            _chaseTimer = 0f;

            if (formerState is PatrollingState patrolState)
                _patrolWasReversed = patrolState.ReversedPath;
        }

        public override void Enter(Enemy enemy)
        {
            SetChaseDestination(enemy);
        }

        public override void Update(Enemy enemy)
        {
            _chaseTimer += Time.deltaTime;
            _pathRecalculationTimer += Time.deltaTime;


            if (IsPlayerInAttackRange(enemy))
            {
                TransitionToAttackState(enemy);
            }
            else if (HasLostPlayer(enemy) || !IsPathValid(enemy))
            {
                TransitionToPatrolState(enemy);
            }
            else if (_pathRecalculationTimer >= _pathRecalculationDelay)
            {
                SetChaseDestination(enemy);
                _pathRecalculationTimer = 0f; // Reset the timer after recalculating the path
            }
        }

        public override void Exit(Enemy enemy)
        {
            // Any cleanup logic when exiting chase state
        }

        bool IsPlayerInAttackRange(Enemy enemy)
        {
            return enemy.IsPlayerInAttackRange();
        }

        void TransitionToAttackState(Enemy enemy)
        {
            enemy.ChangeState(new AttackState(this, _target));
        }

        bool HasLostPlayer(Enemy enemy)
        {
            if (_chaseTimer >= enemy.chaseDuration)
                return true;

            return !enemy.CanSeePlayer() && !enemy.IsPlayerInChaseRange();
        }

        void TransitionToPatrolState(Enemy enemy)
        {
            enemy.StartCoroutine(SearchBeforePatrolling(enemy));
        }

        IEnumerator<WaitForSeconds> SearchBeforePatrolling(Enemy enemy)
        {
            yield return new WaitForSeconds(1f); // Simulate a "searching" phase for 1 second
            enemy.ChangeState(new PatrollingState(this, _patrolWasReversed));
        }

        void SetChaseDestination(Enemy enemy)
        {
            enemy.SetEnemyDestination(enemy.GetPlayerPosition());
        }

        bool IsPathValid(Enemy enemy)
        {
            var path = new NavMeshPath();
            enemy.navMeshAgent.CalculatePath(enemy.GetPlayerPosition(), path);
            return path.status == NavMeshPathStatus.PathComplete;
        }
    }
}
