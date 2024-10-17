using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.Doors.Scripts.Commands
{
    public class CloseDoorCommand : ISimpleCommand
    {
        readonly OpenableObject _openableDoor;

        public CloseDoorCommand(OpenableObject openableDoor)
        {
            _openableDoor = openableDoor;
        }


        public void Execute()
        {
            _openableDoor.SetState(OpenableObject.OpenableState.Closing);
        }
    }
}
