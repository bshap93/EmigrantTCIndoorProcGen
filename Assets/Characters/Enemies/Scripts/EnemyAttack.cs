using System.Collections.Generic;
using Characters.Enemies.Attacks.Commands;
using Characters.Enemies.Attacks.MeeleeAttacks;
using DG.Tweening;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Characters.Enemies.Scripts
{
    public class EnemyAttack : MonoBehaviour
    {
        public float tempMeleeDamage = 1;
        public float tempRangedDamage = 1;

        public GameObject weapon;

        List<DOTweenAnimation> _animations;
        List<IAttackCommand> _attacks;
        void Start()
        {
            _animations = new List<DOTweenAnimation>();
            _attacks = new List<IAttackCommand>();
            _animations.Add(weapon.GetComponent<DOTweenAnimation>());
            _attacks.Add(new SimpleSlashAttack(tempMeleeDamage, _animations[0]));
            InitializeAttacks(weapon.transform);
        }
        void InitializeAttacks(Transform enemyWeapon)
        {
            var doTweenAnimation = enemyWeapon.GetComponent<DOTweenAnimation>();
            _attacks.Add(new SimpleSlashAttack(tempMeleeDamage, doTweenAnimation));
        }

        public IAttackCommand GetAttack()
        {
            return _attacks[0]; // For now, return the first attack
        }

        public void PerformAttack(IDamageable target)
        {
            var attackCommand = GetAttack();
            attackCommand.Execute(target, tempMeleeDamage);
        }
    }
}
