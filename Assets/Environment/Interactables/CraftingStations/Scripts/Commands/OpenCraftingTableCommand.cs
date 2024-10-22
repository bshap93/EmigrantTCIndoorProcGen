using Characters.Player.Scripts;
using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Environment.Interactables.CraftingStations.Scripts.Commands
{
    public class OpenCraftingTableCommand : ISimpleCommand
    {
        readonly ChildSlotsInventory _childSlotsInventory;
        readonly OpenableCraftingTable _openableCraftingTable;

        public OpenCraftingTableCommand(OpenableCraftingTable openableCraftingTable)
        {
            _openableCraftingTable = openableCraftingTable;
            _childSlotsInventory = PlayerCharacter.Instance.GetComponent<ChildSlotsInventory>();
        }
        public void Execute()
        {
            _openableCraftingTable.SetState(OpenableObject.OpenableState.Opening);
            var craftableRecipesGivenInventory =
                _openableCraftingTable.crafter.GetCraftableRecipesGivenInventory(_childSlotsInventory.Peek());

            foreach (var x in craftableRecipesGivenInventory) Debug.Log(x);
        }
    }
}
