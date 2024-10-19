using System.Collections.Generic;
using Characters.Enemies.Attacks.Commands;
using DG.Tweening;
using Environment.ObjectAttributes.Interfaces;
using UnityEngine;

namespace Characters.Enemies.Attacks.MeeleeAttacks
{
    public class SimpleSlashAttack : IAttackCommand
    {
        static readonly int SwordSlash = Animator.StringToHash("SwordSlash");
        readonly float _damage;
        readonly DOTweenAnimation _dotweenAnimation;
        public SimpleSlashAttack(float damage, DOTweenAnimation swordSlash)
        {
            _damage = damage;
            _dotweenAnimation = swordSlash;
        }
        public void Execute(IDamageable target, float dmgValue)
        {
            _dotweenAnimation.DORestart();
            _dotweenAnimation.DOPlay();
            if (target != null) target.TakeDamage(target, dmgValue);
        }
        public float GetDamage()
        {
            return _damage;
        }

        IEnumerator<WaitForSeconds>
            ApplyDamageAfterDelay(IDamageable target, float delay)
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            target.TakeDamage(target, _damage); // Apply the damage
        }
    }
}
