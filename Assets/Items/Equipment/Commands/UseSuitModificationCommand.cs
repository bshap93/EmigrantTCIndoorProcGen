using Characters.Health.Scripts.States;
using Core.Events.EventManagers;

namespace Items.Equipment.Commands
{
    public class UseSuitModificationCommand : IToolUseCommand
    {
        readonly ICharacterEventManager _characterEventManager;
        readonly HealthSystem.SuitModificationType _suitModificationType;

        public UseSuitModificationCommand(HealthSystem.SuitModificationType suitModificationType,
            ICharacterEventManager characterEventManager)
        {
            _suitModificationType = suitModificationType;
            _characterEventManager = characterEventManager;
        }

        public void Execute()
        {
            if (_suitModificationType == HealthSystem.SuitModificationType.FullRepair)
                _characterEventManager.TriggerCharacterSuitModification(HealthSystem.SuitModificationType.FullRepair);
            // Repair the suit
        }
    }
}
