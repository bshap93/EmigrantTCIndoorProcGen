using UnityEngine;

namespace Characters.Enemies.Scripts
{
// Detects when a given target is visible to this object. A target is
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.
    public class EnemyVisiblity : MonoBehaviour
    {
        const float CheckInterval = 0.1f; // Check every 0.2 seconds
        // If the object is more than this distance away, we can't see it.
        public float maxDistance = 10f;

        // The angle of our arc of visibility.
        [Range(0f, 360f)] public float angle = 45f;

        // The object we're looking for.
        [SerializeField] Transform target;
        float _nextCheckTime;

        // A property that other classes can access to determine if we can 
        // currently see our target.
        public bool TargetIsVisible { get; private set; }

        void Start()
        {
            // If we haven't set a target, try to find one.
            if (target == null)
                FindTarget();
        }

        // Check to see if we can see the target every frame.
        void Update()
        {
            if (Time.time >= _nextCheckTime)
            {
                TargetIsVisible = CheckVisibility();
                _nextCheckTime = Time.time + CheckInterval;
            }
        }

        void FindTarget()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        // Returns true if this object can see the specified position.

        // Returns true if a straight line can be drawn between this object and
        // the target. The target must be within range, and be within the
        // visible arc.
        public bool CheckVisibility()
        {
            // Compute the direction to the target
            var directionToTarget = target.position - transform.position;

            // Calculate the number of degrees from the forward direction.
            var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

            // The target is within the arc if it's within half of the specified angle.
            var withinArc = degreesToTarget < angle / 2;

            if (!withinArc) return false;

            // Compute the distance to the point
            var distanceToTarget = directionToTarget.magnitude;

            // Our ray should go as far as the target is, or the maximum distance, whichever is shorter
            var rayDistance = Mathf.Min(maxDistance, distanceToTarget);

            // Create a ray that fires out from our position to the target
            var ray = new Ray(transform.position + Vector3.up, directionToTarget);

            // Store information about what was hit
            RaycastHit hit;
            var canSee = false;

            // Fire the raycast
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // If the ray hits the player, we can see it
                if (hit.collider.transform == target)
                {
                    canSee = true;
                    Debug.DrawLine(transform.position, hit.point, Color.green); // Green means we see the player
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red); // Red means blocked by an obstacle
                }
            }

            return canSee;
        }
    }
}
