using Characters.Health.Scripts.States;
using Characters.Player.Scripts;
using Core.Events.EventManagers;
using Environment.ObjectAttributes.Interfaces;
using Items.Equipment.Commands;
using Polyperfect.Crafting.Integration;

namespace Items.Equipment.Consumables
{
    public class SuitModificationToolHandler : ConsumableHandler
    {
        public HealthSystem.SuitModificationType suitModificationType;
        HealthSystem _healthSystem;
        ChildSlotsInventory _hotbarInventory;
        PlayerEventManager _playerEventManager;


        void Start()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            _hotbarInventory = player.gameObject.GetComponentInChildren<ChildSlotsInventory>();
            _healthSystem = player.GetHealthSystem();
            _playerEventManager = player.gameObject.GetComponent<PlayerEventManager>();
        }

        public override void Use(IDamageable target)
        {
            _healthSystem.RepairSuitHandler(suitModificationType);
            var suitModificationCommand = new UseSuitModificationCommand(suitModificationType, _playerEventManager);
            suitModificationCommand.Execute();
            // _hotbarInventory.RemoveItem(currentItemObejct.ID);
        }

        public override void CeaseUsing()
        {
        }
    }
}
