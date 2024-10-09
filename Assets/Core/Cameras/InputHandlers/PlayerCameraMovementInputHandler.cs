using Cinemachine;
using Core.Cameras.Commands.MoveCamera;
using Core.Cameras.Commands.RotateCamera;
using Core.Cameras.Commands.ZoomCamera;
using UnityEngine;

namespace Core.Cameras.InputHandlers
{
    public class PlayerCameraMovementInputHandler : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        public Transform player;

        public float mouseInfluence = 2f;
        public float maxMouseOffset = 3f;
        public float deadZoneRadius = 50f;
        public float rotateYAmount = 15f;
        public float mouseSensitivity = 100f;
        public float timeBetweenAdjustments = 0.033f;
        [SerializeField] bool isCameraLockedPan;
        [SerializeField] bool isCameraLockedZoom;

        Vector2 _initialMousePosition;

        public float CurrentYRotation { get; private set; }
        public float InitialXRotation { get; private set; }
        public float InitialZRotation { get; private set; }

        void Start()
        {
            InitialXRotation = virtualCamera.transform.rotation.eulerAngles.x;
            InitialZRotation = virtualCamera.transform.rotation.eulerAngles.z;
            CurrentYRotation = virtualCamera.transform.rotation.eulerAngles.y;


            var screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            _initialMousePosition = screenCenter;
        }

        void Update()
        {
            HandleCameraRotation();
            HandleCameraMovement();
            HandleCameraZoom();
        }

        void HandleCameraRotation()
        {
            IRotateCommand rotateCommand = null;

            if (Input.GetKey(KeyCode.E))
                rotateCommand = new RotateClockwiseCommand();
            else if (Input.GetKey(KeyCode.Q)) rotateCommand = new RotateCounterClockwiseCommand();

            if (Input.GetMouseButton(1)) // Right-click to rotate
            {
                var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

                rotateCommand = mouseX switch
                {
                    > 0 => new RotateClockwiseCommand(),
                    < 0 => new RotateCounterClockwiseCommand(),
                    _ => rotateCommand
                };
            }

            rotateCommand?.Execute(virtualCamera, rotateYAmount, timeBetweenAdjustments);
        }

        void HandleCameraMovement()
        {
            if (isCameraLockedPan) return;
            Vector2 currentMousePosition = Input.mousePosition;
            var mouseMovement = currentMousePosition - _initialMousePosition;

            var moveCommand = new MouseCameraMovementCommand(
                mouseMovement, mouseInfluence, maxMouseOffset, deadZoneRadius);

            moveCommand.Execute(virtualCamera, 0);
        }

        void HandleCameraZoom()
        {
            if (isCameraLockedZoom) return;
            switch (Input.mouseScrollDelta.y)
            {
                case > 0:
                {
                    var zoomCommand = new ZoomCommand();
                    zoomCommand.Execute(virtualCamera, 0.5f);
                    break;
                }
                case < 0:
                {
                    var zoomCommand = new ZoomCommand();
                    zoomCommand.Execute(virtualCamera, -0.5f);
                    break;
                }
            }
        }
    }
}
