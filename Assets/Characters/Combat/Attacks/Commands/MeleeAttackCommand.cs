using Characters.Enemies.Attacks.Commands;
using Characters.Scripts;
using UnityEngine;

namespace Combat.Attacks.Commands
{
    public class MeleeAttackCommand : IAttackCommand
    {
        readonly float _damage;
        Transform _attackPoint;
        float _range;

        public MeleeAttackCommand(float damage, float range, Transform attackPoint)
        {
            _damage = damage;
            _range = range;
            _attackPoint = attackPoint;
        }


        public void Execute(IDamageable target, float dmgValue)
        {
        }
        public float GetDamage()
        {
            return _damage;
        }
    }
}
