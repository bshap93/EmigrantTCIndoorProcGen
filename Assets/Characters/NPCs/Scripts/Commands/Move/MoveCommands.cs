using UnityEngine;

namespace Characters.NPCs.Scripts.Commands.Move
{
    // Command for moving the character forward

    public class MoveForwardCommand : MoveCommand
    {
        // Constructor inherited from the base class
        public MoveForwardCommand(Transform cameraTransform, CharacterController controller, float speed)
            : base(cameraTransform, controller, speed)
        {
        }

        // Get the movement vector based on the camera's forward direction
        protected override Vector3 GetMovementVector()
        {
            var forward = CameraTransform.forward;
            forward.y = 0;
            return forward.normalized * Speed * Time.deltaTime;
        }
    }


    // Command for moving the character backward
    public class MoveBackwardCommand : MoveCommand
    {
        // Constructor inherited from the base class
        public MoveBackwardCommand(Transform cameraTransform, CharacterController controller, float speed)
            : base(cameraTransform, controller, speed)
        {
        }

        // Get the movement vector based on the camera's backward direction
        protected override Vector3 GetMovementVector()
        {
            var backward = -CameraTransform.forward;
            backward.y = 0;
            return backward.normalized * Speed * Time.deltaTime;
        }
    }

    // Command for moving the character left
    public class MoveLeftCommand : MoveCommand
    {
        // Constructor inherited from the base class
        public MoveLeftCommand(Transform cameraTransform, CharacterController controller, float speed)
            : base(cameraTransform, controller, speed)
        {
        }

        // Get the movement vector based on the camera's left direction
        protected override Vector3 GetMovementVector()
        {
            var left = -CameraTransform.right;
            left.y = 0;
            return left.normalized * Speed * Time.deltaTime;
        }
    }

    // Command for moving the character right
    public class MoveRightCommand : MoveCommand
    {
        // Constructor inherited from the base class
        public MoveRightCommand(Transform cameraTransform, CharacterController controller, float speed)
            : base(cameraTransform, controller, speed)
        {
        }

        // Get the movement vector based on the camera's right direction
        protected override Vector3 GetMovementVector()
        {
            var right = CameraTransform.right;
            right.y = 0;
            return right.normalized * Speed * Time.deltaTime;
        }
    }
}
