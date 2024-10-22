using DG.Tweening;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class AttackState : EnemyState
    {
        readonly Transform _target;
        float _nextAttackTime; // Tracks when the enemy can attack next
        public AttackState(EnemyState formerState, Transform target) : base(formerState, target)
        {
            _target = target;
        }


        public EnemyState FormerState { get; set; }
        public override void Enter(Enemy enemy)
        {
            enemy.StopMoving();
            _nextAttackTime = Time.time;
        }

        public override void Update(Enemy enemy)
        {
            // Check if enough time has passed since the last attack
            if (Time.time >= _nextAttackTime)
            {
                var attack = enemy.GetAttack();
                enemy.PerformAttack(attack);
                attack.Execute(_target.gameObject.GetComponent<IDamageable>(), attack.GetDamage());

                // Set the next attack time based on the cooldown
                _nextAttackTime = Time.time + enemy.attackCooldown;
            }

            // Transition to chase state if player is out of range
            if (!enemy.IsPlayerInAttackRange())
                enemy.ChangeState(new ChaseState(this, _target));
        }

        public override void Exit(Enemy enemy)
        {
            enemy.StartMoving();
            var doTweenAnimation = enemy.weapon.GetComponent<DOTweenAnimation>();
            doTweenAnimation.DOPause();
        }
    }
}
