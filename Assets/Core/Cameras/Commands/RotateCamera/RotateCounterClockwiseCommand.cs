using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Core.Cameras.Commands.RotateCamera
{
    public class RotateCounterClockwiseCommand : IRotateCommand
    {
        public void Execute(CinemachineVirtualCamera virtualCamera, float value, float timeBetweenAdjustments)
        {
            var initialYRotation = virtualCamera.transform.rotation.eulerAngles.y;
            var initialXRotation = virtualCamera.transform.rotation.eulerAngles.x;
            var initialZRotation = virtualCamera.transform.rotation.eulerAngles.z;

            var targetYRotation = initialYRotation - value;

            // Rotate the virtual camera to the new rotation over 0.5 seconds using DOTween
            virtualCamera.transform.DORotate(
                new Vector3(initialXRotation, targetYRotation, initialZRotation), timeBetweenAdjustments);
        }
    }
}
