using Core.Events;
using UnityEngine;

namespace Characters.NPCs.Scripts.Commands.Move
{
    public class StopMovingCommand : MoveCommand
    {
        public StopMovingCommand(Transform cameraTransform, CharacterController controller, float speed) : base(
            cameraTransform, controller, speed)
        {
        }
        protected override Vector3 GetMovementVector()
        {
            EventManager.EOnCharacterStoppedMoving.Invoke();
            return Vector3.zero;
        }
    }
}
