using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;
using Environment.LevelGeneration.RoomTransition.Doors.Scripts;
using UnityEngine.AI;

namespace Environment.LevelGeneration.Doors.Scripts.Commands.OpenClose
{
    public class OpenHatchCommand : ISimpleCommand
    {
        readonly AutoOpeningConnectingDoor _autoOpeningConnectingDoor;
        readonly NavMeshObstacle _navMeshObstacle;
        public OpenHatchCommand(AutoOpeningConnectingDoor autoOpeningConnectingDoor)
        {
            _autoOpeningConnectingDoor = autoOpeningConnectingDoor;
        }

        public void Execute()
        {
            _autoOpeningConnectingDoor.SetState(OpenableObject.OpenableState.Opening);
        }
    }
}
