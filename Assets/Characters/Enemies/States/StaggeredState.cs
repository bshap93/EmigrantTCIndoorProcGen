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
            enemy.ChangeState(_formerState);
            Debug.Log("Returning from stagger");
        }
    }
}
