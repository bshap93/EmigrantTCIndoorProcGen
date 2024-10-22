using Characters.Health.Scripts.States;

namespace Environment.ObjectAttributes.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(IDamageable dmgeable, float damage);
        HealthSystem GetHealthSystem();
        void Heal(float value);
    }
}
