using Characters.Player.Scripts;
using Cinemachine;
using UnityEngine;

namespace Core.Cameras.Scripts
{
    public class PlayerViewCameraController : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Initialize()
        {
            GetComponent<CinemachineVirtualCamera>().Follow = PlayerCharacter.Instance.transform;
        }
    }
}
