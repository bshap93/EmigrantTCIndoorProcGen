using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Interactables.Triggerables.Scripts
{
    public abstract class TriggerableObject : MonoBehaviour
    {
        [FormerlySerializedAs("IsTriggered")] public bool isTriggered;
        public GameObject eventTriggerCamera;

        public void Trigger()
        {
            if (isTriggered) return;
            isTriggered = true;
            OnTrigger();
        }

        public abstract void OnTrigger();
    }
}
