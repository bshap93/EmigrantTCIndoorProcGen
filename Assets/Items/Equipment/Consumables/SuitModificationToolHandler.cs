using Characters.Health.Scripts.States;
using Characters.Scripts;
using Core.Events.EventManagers;
using Polyperfect.Crafting.Integration;

namespace Items.Equipment.Consumables
{
    public class SuitModificationToolHandler : ConsumableHandler
    {
        public HealthSystem.SuitModificationType suitModificationType;
        public HealthSystem healthSystem;
        public ChildSlotsInventory hotbarInventory;
        PlayerEventManager playerEventManager;

        void Start()
        {
            playerEventManager = FindObjectOfType<PlayerEventManager>();
        }
        public override void Use(IDamageable target)
        {
            healthSystem.RepairSuitHandler(suitModificationType);
            playerEventManager.TriggerCharacterUsedSuitModificationTool(suitModificationType);
            hotbarInventory.RemoveItem(currentItemObejct.ID);
        }

        public override void CeaseUsing()
        {
        }
    }
}
