using Environment.ObjectAttributes.Interfaces;

namespace Characters.Player.Scripts.Commands
{
    public class HealCommand : IStatChangeCommand

    {
        public void Execute(IDamageable target, float value)
        {
            target.Heal(value);
        }
    }

    public interface IStatChangeCommand
    {
        void Execute(IDamageable target, float value);
    }
}
