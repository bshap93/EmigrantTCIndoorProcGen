using System.Collections.Generic;
using Characters.Player.Scripts;
using Core.Cameras.Managers.Scripts;
using Core.Events;
using Core.Utilities.Commands;
using JetBrains.Annotations;
using UI.Objectives.Scripts;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands
{
    public class TriggerTurnOffHazardGasCommand : ISimpleCommand
    {
        readonly GameObject _focus;
        readonly GameObject _hazardGas;
        [CanBeNull] readonly Objective _objectiveCompleted;


        // Optional Variable Objective
        public TriggerTurnOffHazardGasCommand(GameObject hazardGas, GameObject focusObject,
            [CanBeNull] Objective objectiveCompleted = null)
        {
            _hazardGas = hazardGas;

            _objectiveCompleted = objectiveCompleted;

            _focus = focusObject;
        }

        public void Execute()
        {
            if (_objectiveCompleted != null)
            {
            }

            PlayerCharacter.Instance.StartCoroutine(ExtinguishFire());
        }

        IEnumerator<WaitForSeconds> ExtinguishFire()
        {
            var cameraManager = CameraManager.Instance;
            cameraManager.SetFollowObject(_focus);
            yield return new WaitForSeconds(0.5f);
            _hazardGas.SetActive(false);
            EventManager.EOnObjectDestroyed.Invoke(_hazardGas);
            yield return new WaitForSeconds(0.5f);
            cameraManager.SetFollowObject(PlayerCharacter.Instance.gameObject);
        }
    }
}
