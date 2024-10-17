using System;
using Core.ShipSystems.Scripts;
using DunGen;
using Environment.Interactables.Doors.Scripts.Commands;
using Environment.Interactables.Openable.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.LevelGeneration.RoomTransition.Doors.Scripts
{
    public class ManuallyOpenedDoor : OpenableObject, ISystemDependent
    {
        public GameObject movingDoor;
        public Vector3 openOffset;
        [FormerlySerializedAs("_doorComponent")] [CanBeNull]
        public Door doorComponent;
        public bool canBeForcedOpen;
        Vector3 _closedPosition;

        float _currentFramePosition;
        OpenableState _currentState = OpenableState.Closed;

        void Start()
        {
            _closedPosition = movingDoor.transform.localPosition;

            OpenCommand = new OpenDoorCommand(this);
            CloseCommand = new CloseDoorCommand(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentState == OpenableState.Opening || _currentState == OpenableState.Closing) MoveObject();
        }

        public void UpdateSystemStatus(bool hasPower, bool hasAI)
        {
            openable = hasPower || canBeForcedOpen;
        }

        public string Floor { get; }

        public void OpenManuallyOpenedDoor()
        {
            OpenCommand.Execute();
        }

        public void CloseManuallyOpenedDoor()
        {
            CloseCommand.Execute();
        }

        public override void SetState(OpenableState newState)
        {
            _currentState = newState;
            if (newState == OpenableState.Opening)
                if (doorComponent != null)
                    doorComponent.IsOpen = true;
        }
        public override void MoveObject()
        {
            var doorOpenPosition = _closedPosition + openOffset;

            var frameOffset = speed * Time.deltaTime;
            if (_currentState == OpenableState.Closing)
                frameOffset *= -1;

            _currentFramePosition += frameOffset;
            _currentFramePosition = Mathf.Clamp(_currentFramePosition, 0, 1);

            movingDoor.transform.localPosition = Vector3.Lerp(
                _closedPosition, doorOpenPosition, _currentFramePosition);

            // Update state when finished
            if (Mathf.Approximately(_currentFramePosition, 1.0f))
            {
                _currentState = OpenableState.Open;
            }
            else if (Mathf.Approximately(_currentFramePosition, 0))
            {
                _currentState = OpenableState.Closed;
                if (doorComponent != null)
                    doorComponent.IsOpen = false;
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
    }
}
