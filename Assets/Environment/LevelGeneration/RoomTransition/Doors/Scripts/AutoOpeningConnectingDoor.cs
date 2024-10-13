using System;
using Characters.Enemies;
using Core.ShipSystems.Scripts;
using DunGen;
using Environment.Interactables.Openable.Scripts;
using Environment.LevelGeneration.Doors.Scripts.Commands.OpenClose;
using JetBrains.Annotations;
using UnityEngine;

namespace Environment.LevelGeneration.RoomTransition.Doors.Scripts
{
    public class AutoOpeningConnectingDoor : OpenableObject, ISystemDependent

    {
        public GameObject hatchRightHalf;
        [CanBeNull] public GameObject hatchLeftHalf;

        public Vector3 hatchRightOpenOffset = new(-1f, 0f, 0);
        public Vector3 hatchLeftOpenOffset = new(1f, 0f, 0);


        float _currentFramePosition;
        OpenableState _currentState = OpenableState.Closed;
        Door _doorComponent;
        Vector3 _hatchLeftClosedPosition;
        Vector3 _hatchRightClosedPosition;
        
        public string Floor { get; private set; }

        void Awake()
        {
            Floor = GetComponentInParent<FloorIdentifier>()?.floorName ?? "Unknown";
            
        }
        public void UpdateSystemStatus(bool hasPower, bool hasAI)
        {
            if (hasPower)
            {
                openable = true;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            _doorComponent = GetComponent<Door>();
            if (hatchLeftHalf != null) 
                _hatchLeftClosedPosition = hatchLeftHalf.transform.localPosition;
            _hatchRightClosedPosition = hatchRightHalf.transform.localPosition;


            OpenCommand = new OpenHatchCommand(this);
            CloseCommand = new CloseHatchCommand(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentState == OpenableState.Opening || _currentState == OpenableState.Closing) MoveObject();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (agentsAllowedToOpen.Contains(OpenerAgent.Player) && openable)
                if (other.CompareTag("Player"))
                {
                    var playerController = other.GetComponent<CharacterController>();
                    if (playerController == null) return;

                    OpenCommand.Execute();
                }

            if (agentsAllowedToOpen.Contains(OpenerAgent.Enemy) && openable)
                if (other.CompareTag("Enemy"))
                {
                    var enemyController = other.GetComponent<Enemy>();
                    if (enemyController == null) return;

                    OpenCommand.Execute();
                }
        }
        public void OnTriggerExit(Collider other)
        {
            if (agentsAllowedToOpen.Contains(OpenerAgent.Player))
                if (other.CompareTag("Player"))
                {
                    var playerController = other.GetComponent<CharacterController>();
                    if (playerController == null) return;

                    CloseCommand.Execute();
                }

            if (agentsAllowedToOpen.Contains(OpenerAgent.Enemy))
                if (other.CompareTag("Enemy"))
                {
                    var enemyController = other.GetComponent<Enemy>();
                    if (enemyController == null) return;

                    CloseCommand.Execute();
                }
        }


        public override void MoveObject()
        {
            var hatchLeftOpenPosition = _hatchLeftClosedPosition + hatchLeftOpenOffset;
            var hatchRightOpenPosition = _hatchRightClosedPosition + hatchRightOpenOffset;

            var frameOffset = speed * Time.deltaTime;
            if (_currentState == OpenableState.Closing)
                frameOffset *= -1;

            _currentFramePosition += frameOffset;
            _currentFramePosition = Mathf.Clamp(_currentFramePosition, 0, 1);

            if (hatchLeftHalf != null)
                hatchLeftHalf.transform.localPosition = Vector3.Lerp(
                    _hatchLeftClosedPosition, hatchLeftOpenPosition, _currentFramePosition);

            hatchRightHalf.transform.localPosition = Vector3.Lerp(
                _hatchRightClosedPosition, hatchRightOpenPosition, _currentFramePosition);

            // Update state when finished
            if (Mathf.Approximately(_currentFramePosition, 1.0f))
            {
                _currentState = OpenableState.Open;
            }
            else if (Mathf.Approximately(_currentFramePosition, 0))
            {
                _currentState = OpenableState.Closed;
                _doorComponent.IsOpen = false;
            }
        }
        public override void Open()
        {
            throw new NotImplementedException();
        }
        public override void Close()
        {
            throw new NotImplementedException();
        }


        public override void SetState(OpenableState newState)
        {
            _currentState = newState;
            if (newState == OpenableState.Opening) _doorComponent.IsOpen = true;
        }

    }
}
