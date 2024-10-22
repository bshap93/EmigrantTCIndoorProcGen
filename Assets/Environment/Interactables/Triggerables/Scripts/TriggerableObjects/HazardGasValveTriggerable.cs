using Environment.Interactables.Triggerables.Scripts.Commands;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class HazardGasValveTriggerable : TriggerableObject
    {
        public GameObject focusObject;
        public GameObject hazardGas;
        void Start()
        {
            isTriggered = false;
        }
        public override void OnTrigger()
        {
            var command = new TriggerTurnOffHazardGasCommand(hazardGas, focusObject);
            command.Execute();
        }
    }
}
