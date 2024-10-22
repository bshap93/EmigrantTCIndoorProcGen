using DG.Tweening;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class BrokenDoorPaneTriggerable : TriggerableObject
    {
        public GameObject doorObject;
        public Vector3 openOffset;

        void Start()
        {
            isTriggered = false;
        }


        public override void OnTrigger()
        {
            Debug.Log("BrokenDoorPaneTriggerable triggered");
            doorObject.transform.DOMove(doorObject.transform.position + openOffset, 1f);
        }
    }
}
