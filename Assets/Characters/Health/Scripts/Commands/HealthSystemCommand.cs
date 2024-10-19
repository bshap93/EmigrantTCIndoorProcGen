using Core.Events.EventManagers;
using Environment.ObjectAttributes.Interfaces;

namespace Characters.Health.Scripts.Commands
{
    public interface IHealthSystemCommand
    {
        void Execute(IDamageable damageable, float value, ICharacterEventManager eventManager);
    }
}
