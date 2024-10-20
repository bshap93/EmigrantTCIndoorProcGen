using Environment.Interactables.Triggerables.Scripts.Commands;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class HazardGasValveTriggerable : TriggerableObject
    {
        public GameObject gasHazardsTurnedOffByThisValve;
        void Start()
        {
            isTriggered = false;
        }
        public override void OnTrigger()
        {
            var command = new TriggerTurnOffHazardGasCommand(gasHazardsTurnedOffByThisValve);
            command.Execute();
        }
    }
}
