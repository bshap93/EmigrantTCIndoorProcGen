using Environment.ObjectAttributes.Interfaces;

namespace Characters.Enemies.Attacks.Commands
{
    public class StopAttackCommand : IAttackCommand
    {
        public void Execute(IDamageable target, float dmgValue)
        {
        }
        public float GetDamage()
        {
            return 0;
        }
    }
}
