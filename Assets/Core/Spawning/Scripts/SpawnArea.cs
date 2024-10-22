using UnityEngine;

namespace Core.Spawning.Scripts
{
    public class SpawnArea : MonoBehaviour
    {
        public Vector3 areaSize; // Size of the spawn area in X, Y, Z
        public LayerMask obstacleLayer;

        public Vector3 GetRandomSpawnPosition()
        {
            var randomPosition = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2), // Consider height (Y axis)
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            // Ensure that the random position is valid (e.g., not inside a wall or obstacle)
            if (Physics.CheckSphere(
                    randomPosition, 1f, obstacleLayer)) return GetRandomSpawnPosition(); // Retry if invalid

            return randomPosition;
        }
    }
}
