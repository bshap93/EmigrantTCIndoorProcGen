using DunGen;
using Environment.Interactables.Openable.Scripts;
using Environment.LevelGeneration.Doors.Scripts.Commands.OpenClose;
using UnityEngine;
using UnityEngine.AI;

namespace Environment.LevelGeneration.Doors.Scripts
{
    public class AutoOpeningConnectingDoor : OpenableObject

    {
        public enum DoorState
        {
            Open,
            Closed,
            Opening,
            Closing
        }


        public GameObject hatchRightHalf;
        public GameObject hatchLeftHalf;

        public OpenableState defaultState;

        public Vector3 hatchRightOpenOffset = new(-1f, 0f, 0);
        public Vector3 hatchLeftOpenOffset = new(1f, 0f, 0);

        OpenableState _currentState;
        Door _doorComponent;
        Vector3 _hatchLeftClosedPosition;
        Vector3 _hatchRightClosedPosition;
        NavMeshObstacle _navMeshObstacle;


        // Start is called before the first frame update
        void Start()
        {
            _doorComponent = GetComponent<Door>();
            _hatchLeftClosedPosition = hatchLeftHalf.transform.localPosition;
            _hatchRightClosedPosition = hatchRightHalf.transform.localPosition;
            _navMeshObstacle = GetComponent<NavMeshObstacle>();


            OpenCommand = new OpenHatchCommand(this, _navMeshObstacle);
            CloseCommand = new CloseHatchCommand(this, _navMeshObstacle);
            _currentState = defaultState;

            SetState(_currentState);

            if (_currentState == OpenableState.Open) OpenCommand.Execute();
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentState == OpenableState.Opening || _currentState == OpenableState.Closing) MoveObject();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<CharacterController>();
                if (playerController == null) return;

                // EventManager.EDoorOpened.Invoke();

                OpenCommand.Execute();
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<CharacterController>();
                if (playerController == null) return;

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


        public override void SetState(OpenableState newState)
        {
            _currentState = newState;
            if (newState == OpenableState.Opening) _doorComponent.IsOpen = true;
        }
    }
}
