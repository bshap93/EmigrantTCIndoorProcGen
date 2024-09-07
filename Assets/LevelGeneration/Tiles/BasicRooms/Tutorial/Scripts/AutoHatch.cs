using DunGen;
using UnityEngine;

namespace LevelGeneration.Tiles.BasicRooms.Tutorial.Scripts
{
    public class AutoHatch : MonoBehaviour
    {
        public enum DoorState
        {
            Open,
            Closed,
            Opening,
            Closing
        }

        public float speed = 3.0f;
        public GameObject hatchRightHalf;
        public GameObject hatchLeftHalf;

        public Vector3 hatchRightOpenOffset = new(0, 2.5f, 0);
        public Vector3 hatchLeftOpenOffset = new(0, 2.5f, 0);
        float _currentFramePosition;


        DoorState _currentState = DoorState.Closed;
        Door _doorComponent;
        Vector3 _hatchLeftClosedPosition;

        Vector3 _hatchRightClosedPosition;
        // Start is called before the first frame update
        void Start()
        {
            _doorComponent = GetComponent<Door>();
            _hatchLeftClosedPosition = hatchLeftHalf.transform.localPosition;
            _hatchRightClosedPosition = hatchRightHalf.transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentState == DoorState.Opening || _currentState == DoorState.Closing)
            {
                var hatchLeftOpenPosition = _hatchLeftClosedPosition + hatchLeftOpenOffset;
                var hatchRightOpenPosition = _hatchRightClosedPosition + hatchRightOpenOffset;

                var frameOffset = speed * Time.deltaTime;

                if (_currentState == DoorState.Closing)
                    frameOffset *= -1;

                _currentFramePosition += frameOffset;
                _currentFramePosition = Mathf.Clamp(_currentFramePosition, 0, 1);

                hatchLeftHalf.transform.localPosition = Vector3.Lerp(
                    _hatchLeftClosedPosition, hatchLeftOpenPosition, _currentFramePosition);

                hatchRightHalf.transform.localPosition = Vector3.Lerp(
                    _hatchRightClosedPosition, hatchRightOpenPosition, _currentFramePosition);

                // Finished
                if (Mathf.Approximately(_currentFramePosition, 1.0f))
                {
                    _currentState = DoorState.Open;
                }
                else if (Mathf.Approximately(_currentFramePosition, 0))
                {
                    _currentState = DoorState.Closed;
                    _doorComponent.IsOpen = false;
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // if Tag is Player
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<CharacterController>();

                // Ignore overlaps with anything other than the player
                if (playerController == null)
                    return;

                _currentState = DoorState.Opening;
                _doorComponent.IsOpen = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // if Tag is Player
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<CharacterController>();

                // Ignore overlaps with anything other than the player
                if (playerController == null)
                    return;

                _currentState = DoorState.Closing;
            }
        }
    }
}
