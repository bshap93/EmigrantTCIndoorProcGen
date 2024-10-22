using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.Chests.Scripts.Commands
{
    public class CloseChestCommand : ISimpleCommand
    {
        readonly OpenableChest _openableDoor;

        public CloseChestCommand(OpenableChest openableDoor)
        {
            _openableDoor = openableDoor;
        }
        public void Execute()
        {
            _openableDoor.SetState(OpenableObject.OpenableState.Closing);
        }
    }
}
