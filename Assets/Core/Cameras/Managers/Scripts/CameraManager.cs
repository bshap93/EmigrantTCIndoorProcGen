using System;
using Characters.Player.Scripts;
using Cinemachine;
using Core.Cameras.Scripts;
using DG.Tweening;
using UnityEngine;

namespace Core.Cameras.Managers.Scripts
{
    public enum CameraTypeEnum
    {
        Player = 10,
        EventTriggered = 5
    }

    public class CameraManager : MonoBehaviour
    {
        public GameObject playerCamera;
        PlayerCharacter _player;

        PlayerViewCameraController _playerViewCameraController;


        public static CameraManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep the manager across scenes
            }
            else
            {
                Destroy(gameObject); // Prevent duplicates
            }
        }

        void Start()
        {
            if (_player == null) _player = PlayerCharacter.Instance;
            // Subscribe to the player damage event
            _player.playerEventManager.AddListenerToHealthChangedEvent(OnPlayerDamage);
            _playerViewCameraController = GetComponentInChildren<PlayerViewCameraController>();
            _playerViewCameraController.Initialize();
        }

        public void SetActiveCamera(CameraTypeEnum virtualCamera)
        {
            switch (virtualCamera)
            {
                case CameraTypeEnum.Player:
                    playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    break;
            }
        }

        void OnPlayerDamage(float damage, bool isDamage)
        {
            ShakeCamera(0.5f, 0.5f, 10, 90);
        }


        public void ShakeCamera(float duration, float strength, int vibrato, float randomness)
        {
            var cameraOffset = playerCamera.GetComponent<CinemachineCameraOffset>();
            // Use DOTween to shake the camera offset position for a shaking effect
            DOTween.Shake(
                    () => cameraOffset.m_Offset,
                    x => cameraOffset.m_Offset = x,
                    duration, strength, vibrato, randomness)
                .SetEase(Ease.OutQuad);
        }

        public void SetFollowObject(GameObject followObject)
        {
            playerCamera.GetComponent<CinemachineVirtualCamera>().Follow = followObject.transform;
        }

        public void SetActiveRoom(GameObject room)
        {
            throw new NotImplementedException();
        }
    }
}
