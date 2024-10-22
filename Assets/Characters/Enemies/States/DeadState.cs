using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class DeadState : EnemyState
    {
        static readonly int Dead = Animator.StringToHash("Dead");
        readonly Animator _animator;
        public DeadState(Animator animator, [CanBeNull] EnemyState formerState) : base(
            formerState, null)
        {
            _animator = animator;
        }
        public override void Enter(Enemy enemy)
        {
            enemy.SetDead();
            _animator.SetTrigger(Dead);
            Debug.Log("Enemy is dead");
        }
        public override void Update(Enemy enemy)
        {
        }
        public override void Exit(Enemy enemy)
        {
        }
    }
}
