using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class DeadState : EnemyState
    {
        static readonly int Dead = Animator.StringToHash("Dead");
        public DeadState(Animator animator, [CanBeNull] EnemyState formerState) : base(
            formerState, null)
        {
            animator.SetBool(Dead, true);
        }
        public override void Enter(Enemy enemy)
        {
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
