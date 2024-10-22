using Environment.Interactables.Doors.Scripts.Commands;
using Environment.Interactables.Openable.Scripts;
using UnityEngine;

namespace Environment.Interactables.Doors.Scripts
{
    public class OpenableDoor : OpenableObject
    {
        public GameObject doorObject01;

        public Vector3 door01OpenPositionOffset = new(0, 0.5f, 0);
        public Vector3 door01OpenRotationOffset = new(0, 0, 0);


        Vector3 _door01ClosedPosition;
        Vector3 _door01ClosedRotation;

        Vector3 _door02ClosedPosition;
        Vector3 _door02ClosedRotation;


        void Start()
        {
            _door01ClosedPosition = doorObject01.transform.localPosition;
            _door01ClosedRotation = doorObject01.transform.localEulerAngles;
            CurrentState = OpenableState.Closed;
            OpenCommand = new OpenDoorCommand(this);
            CloseCommand = new CloseDoorCommand(this);
            openingMechanism = OpeningMechanism.Button;
        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentState == OpenableState.Opening || CurrentState == OpenableState.Closing) MoveObject();
        }

        public override void Close()
        {
            // if (CurrentState == OpenableState.Open || CurrentState == OpenableState.Opening)
            CloseCommand.Execute();
        }

        public override void Open()
        {
            Debug.Log("Player opened door  trigger");

            // if (CurrentState == OpenableState.Closed || CurrentState == OpenableState.Closing)

            OpenCommand.Execute();
        }

        public override void MoveObject()
        {
            var door01OpenPosition = _door01ClosedPosition + door01OpenPositionOffset;
            var door01OpenRotation = _door01ClosedRotation + door01OpenRotationOffset;

            var frameOffset = speed * Time.deltaTime;
            if (CurrentState == OpenableState.Closing)
                frameOffset *= -1;

            CurrentFramePosition += frameOffset;
            CurrentFramePosition = Mathf.Clamp(CurrentFramePosition, 0, 1);

            doorObject01.transform.localPosition = Vector3.Lerp(
                _door01ClosedPosition, door01OpenPosition, CurrentFramePosition);

            doorObject01.transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(_door01ClosedRotation), Quaternion.Euler(door01OpenRotation),
                CurrentFramePosition);


            // Update state when finished
            if (Mathf.Approximately(CurrentFramePosition, 1.0f))
                CurrentState = OpenableState.Open;
            else if (Mathf.Approximately(CurrentFramePosition, 0)) CurrentState = OpenableState.Closed;
        }
        public override void SetState(OpenableState newState)
        {
            CurrentState = newState;
        }
    }
}
