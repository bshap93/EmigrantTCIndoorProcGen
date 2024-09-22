using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class StaggeredState : EnemyState
    {
        static readonly int Staggered = Animator.StringToHash("Staggered");
        readonly Animator _animator;
        readonly EnemyState _formerState;
        readonly float _staggerDuration;
        public StaggeredState(Animator animator, EnemyState formerState, float staggerDuration,
            [CanBeNull] Transform target) : base(
            formerState, target)
        {
            _animator = animator;
            _formerState = formerState;
            _staggerDuration = staggerDuration;
        }
        public override void Enter(Enemy enemy)
        {
            _animator.SetBool(Staggered, true);
            enemy.StartCoroutine(ReturnFromStaggerAfterDelay(enemy));
        }
        public override void Update(Enemy enemy)
        {
        }
        public override void Exit(Enemy enemy)
        {
            _animator.SetBool(Staggered, false);
        }

        IEnumerator<WaitForSeconds> ReturnFromStaggerAfterDelay(Enemy enemy)
        {
            yield return new WaitForSeconds(_staggerDuration);
            // If the enemy was patrolling before being staggered, start chase state
            if (_formerState is PatrollingState) enemy.ChangeState(new ChaseState(this, enemy.player));
            // If the enemy was chasing before being staggered, return to chase state
            else if (_formerState is ChaseState) enemy.ChangeState(_formerState);
            // If the enemy was attacking before being staggered, return to attack state
            else if (_formerState is AttackState) enemy.ChangeState(_formerState);
            // Else, return to former state
            else
                enemy.ChangeState(_formerState);

            Debug.Log("Returning from stagger");
        }
    }
}
