using Characters.Command;
using DG.Tweening;
using UnityEngine;

// Include DOTween namespace

namespace Characters.Player.Scripts.Commands.Rotate
{
    public class RotateCommand : BaseCommand
    {
        readonly Vector3? _absoluteDirection;
        readonly float _duration; // Add duration for smooth rotation

        // Constructor for absolute rotation with duration
        public RotateCommand(float angle, Vector3 cameraForward, float duration = 0.5f)
        {
            var absoluteDirection = Quaternion.Euler(0, angle, 0) * cameraForward;
            _absoluteDirection = absoluteDirection;
            _duration = duration; // Set duration
        }

        public override void Execute(GameObject actor)
        {
            if (_absoluteDirection.HasValue)
            {
                // Absolute rotation: rotate smoothly to face a specific direction
                var targetRotation = Quaternion.LookRotation(_absoluteDirection.Value);
                actor.transform.DORotateQuaternion(targetRotation, _duration)
                    .SetEase(Ease.InOutSine); // Smooth ease transition
            }
        }
    }
}
