﻿using UnityEditor;
using UnityEngine;

namespace Characters.Enemies.Scripts
{
// Detects when a given target is visible to this object. A target is
// visible when it's both in range and in front of the target. Both the
// range and the angle of visibility are configurable.
    public class EnemyVisiblity : MonoBehaviour
    {
        // If the object is more than this distance away, we can't see it.
        public float maxDistance = 10f;

        // The angle of our arc of visibility.
        [Range(0f, 360f)] public float angle = 45f;

        // If true, visualise changes in visilibity by changing material colour
        [SerializeField] bool visualize = true;
        // The object we're looking for.
        [SerializeField] Transform target;

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
            TargetIsVisible = CheckVisibility();
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

#if UNITY_EDITOR
// A custom editor for the EnemyVisibility class. Visualises and allows
// editing the visible range.
    [CustomEditor(typeof(EnemyVisiblity))]
    public class EnemyVisibilityEditor : Editor
    {
        // Called when Unity needs to draw the Scene view. 
        void OnSceneGUI()
        {
            // Get a reference to the EnemyVisibility script we're looking at
            var visibility = target as EnemyVisiblity;

            // Start drawing at 10% opacity
            Handles.color = new Color(1, 1, 1, 0.1f);

            // Drawing an arc sweeps from the point you give it. We want to
            // draw the arc such that the middle of the arc is in front of the
            // object, so we'll take the forward direction and rotate it by
            // half the angle.

            var forwardPointMinusHalfAngle =
                // rotate around the Y axis by half the angle
                Quaternion.Euler(0, -visibility.angle / 2, 0)
                // rotate the forward direction by this
                * visibility.transform.forward;

            // Draw the arc to visualise the visibility arc
            var arcStart =
                forwardPointMinusHalfAngle * visibility.maxDistance;

            Handles.DrawSolidArc(
                visibility.transform.position, // The center of the arc
                Vector3.up, // The up-direction of the arc
                arcStart, // The point where it begins
                visibility.angle, // The angle of the arc
                visibility.maxDistance // The radius of the arc
            );


            // Draw a scale handle at the edge of the arc; if the user drags
            // it, update the arc size.

            // Reset the handle colour to full opacity
            Handles.color = Color.white;

            // Compute the position of the handle, based on the object's
            // position, the direction it's facing, and the distance
            var handlePosition =
                visibility.transform.position +
                visibility.transform.forward * visibility.maxDistance;

            // Draw the handle, and store its result.
            visibility.maxDistance = Handles.ScaleValueHandle(
                visibility.maxDistance, // current value
                handlePosition, // handle position
                visibility.transform.rotation, // orientation
                1, // size
                Handles.ConeHandleCap, // cap to draw
                0.25f); // snap to multiples of this 
            // if the snapping key is
            // held down
        }
    }
#endif
// END enemy_visibility
}