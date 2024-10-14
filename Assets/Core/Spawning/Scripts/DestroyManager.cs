using System.Collections.Generic;
using UnityEngine;

namespace Core.Spawning.Scripts
{
    public class DestroyManager : MonoBehaviour
    {
        public readonly Dictionary<string, int> NeededNumberToDestroyByTag = new();
        public Dictionary<string, int> NumDestroyedObjectsByTag = new();

        public void AddTag(string destroyedObjectTag, int numNeededToDestroy)
        {
            NeededNumberToDestroyByTag[destroyedObjectTag] = numNeededToDestroy;
            NumDestroyedObjectsByTag[destroyedObjectTag] = 0;
        }

        public void IncrementDestroyedObject(string destroyedObjectTag)
        {
            NumDestroyedObjectsByTag[destroyedObjectTag]++;
            Debug.Log($"Incremented {destroyedObjectTag} to {NumDestroyedObjectsByTag[destroyedObjectTag]}");
        }

        public bool CheckIfObjectiveIsComplete(string objectTag)
        {
            if (NumDestroyedObjectsByTag[objectTag] < NeededNumberToDestroyByTag[objectTag])
                return false;

            return true;
        }
    }
}
