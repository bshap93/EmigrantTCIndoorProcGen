using UnityEngine;

namespace Core.Spawning.Scripts
{
    public class SpawnPoint25D : MonoBehaviour
    {
        public bool isOccupied;

        public bool CanSpawn()
        {
            return !isOccupied; // Allow spawning only if the spawn point is free
        }

        public void MarkOccupied()
        {
            isOccupied = true;
        }

        public void Clear()
        {
            isOccupied = false;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                transform.position.x, 0f, transform.position.z); // Flat spawn position (no Y-axis movement)
        }
    }
}
