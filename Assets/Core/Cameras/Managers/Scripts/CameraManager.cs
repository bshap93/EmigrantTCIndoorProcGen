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
        Room = 5
    }

    public class CameraManager : MonoBehaviour
    {
        public GameObject playerCamera;
        public GameObject roomCamera;
        public PlayerCharacter player;

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

            // SetActiveCamera(0);
        }

        void Start()
        {
            if (player == null) player = PlayerCharacter.Instance;
            // Subscribe to the player damage event
            player.playerEventManager.AddListenerToHealthChangedEvent(OnPlayerDamage);
            _playerViewCameraController = GetComponentInChildren<PlayerViewCameraController>();
            _playerViewCameraController.Initialize();
        }

        public void SetActiveCamera(CameraTypeEnum virtualCamera)
        {
            switch (virtualCamera)
            {
                case CameraTypeEnum.Player:
                    playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                    roomCamera.GetComponent<CinemachineVirtualCamera>().Priority = 5;
                    break;
                case CameraTypeEnum.Room:
                    playerCamera.GetComponent<CinemachineVirtualCamera>().Priority = 5;
                    roomCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;

                    break;
            }
        }

        void OnPlayerDamage(float damage, bool isDamage)
        {
            ShakeCamera(0.5f, 0.5f, 10, 90);
        }

        public void SetActiveRoom(GameObject room)
        {
            throw new NotImplementedException();
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
    }
}