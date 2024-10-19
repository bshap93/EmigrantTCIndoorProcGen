using Characters.Enemies.Attacks.Commands;
using Environment.ObjectAttributes.Interfaces;

namespace Characters.Enemies.Attacks.RangedAttacks
{
    public class BasicRangedAttackCommand : IAttackCommand
    {
        float _damage;
        public void Execute(IDamageable target, float dmgValue)
        {
            _damage = dmgValue;
            if (target != null)
                target.TakeDamage(target, dmgValue);
        }
        public float GetDamage()
        {
            return _damage;
        }
    }
}
