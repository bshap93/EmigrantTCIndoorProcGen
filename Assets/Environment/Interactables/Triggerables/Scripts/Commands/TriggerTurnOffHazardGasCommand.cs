using Core.Utilities.Commands;
using JetBrains.Annotations;
using UI.Objectives.Scripts;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands
{
    public class TriggerTurnOffHazardGasCommand : ISimpleCommand
    {
        readonly GameObject[] _hazardGas;
        [CanBeNull] readonly Objective _objectiveCompleted;


        // Optional Variable Objective
        public TriggerTurnOffHazardGasCommand(GameObject[] hazardGas, [CanBeNull] Objective objectiveCompleted = null)
        {
            _hazardGas = hazardGas;

            _objectiveCompleted = objectiveCompleted;
        }
        public void Execute()
        {
            foreach (var gas in _hazardGas) gas.SetActive(false);
            Debug.Log("TriggerTurnOffHazardGasCommand");
            if (_objectiveCompleted != null)
            {
            }
        }
    }
}
