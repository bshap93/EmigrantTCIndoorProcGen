using UnityEngine;
using UnityEngine.AI;

namespace Characters.NPCs.Scripts
{
    public class NavMeshCharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed; // Set the movement speed of the NavMeshAgent
        }

        void Update()
        {
            // Get input for movement
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            // Create a movement vector
            var movement = new Vector3(horizontal, 0, vertical).normalized;

            if (movement.magnitude >= 0.1f)
            {
                // Calculate the target position the agent should move to
                var moveDirection = transform.position + movement;

                // Set the agent's destination to the calculated position
                agent.SetDestination(moveDirection);

                // Rotate the character to face the movement direction
                var targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
