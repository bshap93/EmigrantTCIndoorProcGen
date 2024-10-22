using Cinemachine;

namespace Core.Cameras.Commands.RotateCamera
{
    public interface IRotateCommand
    {
        void Execute(CinemachineVirtualCamera virtualCamera, float value, float timeBetweenAdjustments);
    }
}
