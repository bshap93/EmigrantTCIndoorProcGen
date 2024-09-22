using Characters.Health.Scripts;

namespace Characters.Scripts
{
    public interface IDamageable
    {
        void TakeDamage(IDamageable damageable, float damage);
        HealthSystem GetHealthSystem();
        void Heal(float value);
        void StartTurn();
    }
}
