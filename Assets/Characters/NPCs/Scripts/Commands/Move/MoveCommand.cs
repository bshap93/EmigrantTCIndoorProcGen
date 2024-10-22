using Characters.Command;
using UnityEngine;

namespace Characters.NPCs.Scripts.Commands.Move
{
    public abstract class MoveCommand : BaseCommand
    {
        // Gravity and vertical velocity handling
        const float Gravity = -9.81f;
        protected readonly Transform CameraTransform;
        protected readonly CharacterController Controller;
        protected readonly float Speed;
        float _verticalVelocity;


        protected MoveCommand(Transform cameraTransform, CharacterController controller, float speed)
        {
            CameraTransform = cameraTransform;
            Controller = controller;
            Speed = speed;
        }

        public override void Execute(GameObject actor)
        {
            ApplyGravity();
            var movement = GetMovementVector();
            movement = ApplyVerticalMovement(movement);
            Controller.Move(movement);
        }

        // Abstract method for direction-specific movement
        protected abstract Vector3 GetMovementVector();

        // Apply gravity if the character is not grounded
        protected void ApplyGravity()
        {
            if (Controller.isGrounded)
                _verticalVelocity = -0.5f; // Slight negative value to keep the player grounded
            else
                _verticalVelocity += Gravity * Time.deltaTime;
        }

        // Apply vertical movement based on gravity
        protected Vector3 ApplyVerticalMovement(Vector3 movement)
        {
            movement.y = _verticalVelocity * Time.deltaTime;
            return movement;
        }
    }
}
