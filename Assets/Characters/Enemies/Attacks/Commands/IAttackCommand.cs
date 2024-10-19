using Environment.ObjectAttributes.Interfaces;
using JetBrains.Annotations;

namespace Characters.Enemies.Attacks.Commands
{
    public interface IAttackCommand
    {
        void Execute([CanBeNull] IDamageable target, float dmgValue);
        float GetDamage();
    }
}
