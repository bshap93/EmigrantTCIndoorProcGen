using Characters.Health.Scripts.States;
using Characters.Scripts;
using Core.Events.EventManagers;
using Items.Equipment.Commands;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Equipment.Consumables
{
    public class SuitModificationToolHandler : ConsumableHandler
    {
        public HealthSystem.SuitModificationType suitModificationType;
        public HealthSystem healthSystem;
        public ChildSlotsInventory hotbarInventory;
        [SerializeField] PlayerEventManager _playerEventManager;


        public override void Use(IDamageable target)
        {
            healthSystem.RepairSuitHandler(suitModificationType);
            var suitModificationCommand = new UseSuitModificationCommand(suitModificationType, _playerEventManager);
            suitModificationCommand.Execute();
            hotbarInventory.RemoveItem(currentItemObejct.ID);
        }

        public override void CeaseUsing()
        {
        }
    }
}
