using Cinemachine;
using UnityEngine;

namespace Core.Cameras.Commands.ZoomCamera
{
    public class ZoomCommand : ICameraCommand
    {
        // Positive value zooms in, negative value zooms out
        public void Execute(CinemachineVirtualCamera virtualCamera, float value)
        {
            var cinemachineCameraOffset = virtualCamera.gameObject.GetComponent<CinemachineCameraOffset>();

            var newZOffset = Mathf.Clamp(cinemachineCameraOffset.m_Offset.z + value, 0f, 4f);
            cinemachineCameraOffset.m_Offset.z = newZOffset;
        }
    }
}
