using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.CraftingStations.Scripts.Commands
{
    public class CloseCraftingTableCommand : ISimpleCommand
    {
        readonly OpenableCraftingTable _openableCraftingTable;

        public CloseCraftingTableCommand(OpenableCraftingTable openableCraftingTable)
        {
            _openableCraftingTable = openableCraftingTable;
        }
        public void Execute()
        {
            _openableCraftingTable.SetState(OpenableObject.OpenableState.Closing);
        }
    }
}
