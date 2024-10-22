using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.Chests.Scripts.Commands
{
    public class OpenChestCommand : ISimpleCommand
    {
        readonly OpenableChest _openableDoor;

        public OpenChestCommand(OpenableChest openableDoor)
        {
            _openableDoor = openableDoor;
        }


        public void Execute()
        {
            _openableDoor.SetState(OpenableObject.OpenableState.Opening);
        }
    }
}
