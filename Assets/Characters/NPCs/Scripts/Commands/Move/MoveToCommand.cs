using Characters.Command;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

// Include DoTween

namespace Characters.Scripts.Commands.Move
{
    public class MoveToCommand : BaseCommand
    {
        readonly Vector3 _destination;
        readonly NavMeshPath _path;
        readonly float _speed;
        int _pathIndex;

        // Constructor for the move command
        public MoveToCommand(Vector3 destination, float speed)
        {
            _destination = destination;
            _speed = speed;
            _path = new NavMeshPath();
        }

        public override void Execute(GameObject actor)
        {
            var controller = actor.GetComponent<CharacterController>();

            if (controller == null)
            {
                UnityEngine.Debug.LogError("No CharacterController found on the actor.");
                return;
            }

            // Calculate the NavMesh path to the destination
            NavMesh.CalculatePath(actor.transform.position, _destination, NavMesh.AllAreas, _path);

            if (_path.status == NavMeshPathStatus.PathComplete)
            {
                _pathIndex = 0; // Start at the first waypoint
                MoveAlongPathWithDoTween(actor); // Move with DoTween
            }
            else
            {
                UnityEngine.Debug.LogWarning("Path could not be calculated.");
            }
        }

        // Use DoTween to move along the path smoothly
        void MoveAlongPathWithDoTween(GameObject actor)
        {
            if (_path == null || _path.corners.Length == 0)
            {
                UnityEngine.Debug.LogWarning("Path is invalid or empty.");
                return;
            }

            if (_pathIndex < _path.corners.Length)
            {
                var targetPosition = _path.corners[_pathIndex];

                // Use DoTween to smoothly move the actor to the target position
                actor.transform.DOMove(targetPosition, _speed)
                    .SetSpeedBased(true) // Adjust speed based on distance
                    .SetEase(Ease.Linear) // Use a linear movement ease
                    .OnComplete(
                        () =>
                        {
                            _pathIndex++; // Move to the next waypoint when completed
                            if (_pathIndex < _path.corners.Length)
                                MoveAlongPathWithDoTween(actor); // Move to the next point
                        });
            }
        }
    }
}
