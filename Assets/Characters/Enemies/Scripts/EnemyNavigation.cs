using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.Scripts
{
    public class EnemyNavigation : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;


        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        public bool HasReachedDestination()
        {
            return !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
        }

        public void StopMoving()
        {
            navMeshAgent.isStopped = true;
        }

        public void StartMoving()
        {
            navMeshAgent.isStopped = false;
        }
    }
}
