using Environment.Interactables.Triggerables.Scripts.Commands;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class BrokenDoorPaneTriggerable : TriggerableObject
    {
        public GameObject doorOpenedByThisTrigger;

        void Start()
        {
            isTriggered = false;
        }


        public override void OnTrigger()
        {
            var command = new TriggerOpenDoorCommand();
            command.Execute();
        }
    }
}
