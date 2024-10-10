using System;
using Characters.Health.Scripts.States.OxygenState;
using Core.Events.EventManagers;
using UI.InGameConsole.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters.Health.Scripts.States
{
    public class HealthSystem : MonoBehaviour
    {
        [Serializable]
        public enum OxygenState
        {
            Stable,
            Leaking
        }

        public enum SuitModificationType
        {
            FullRepair,
            PartialRepair,
            Upgrade
        }


        public const float MaxOxygen = 100;
        public const float MaxSuitIntegrity = 100;

        public string characterName;

        public float currentSuitIntegrity;
        public float currentOxygen;
        [FormerlySerializedAs("oxygenState")] public OxygenState oxygenStateInitial;
        public float oxygenDepletionRate;
        // Event to notify observers when health changes

        [FormerlySerializedAs("_inGameConsoleManager")]
        public InGameConsoleManager inGameConsoleManager;
        ICharacterEventManager _characterEventManager;

        IOxygenState _oxygenState;


        void Start()
        {
            _characterEventManager = gameObject.GetComponent<ICharacterEventManager>();


            UnityAction<float, bool> oxygenChange = OnOxygenChangedHandler;
            _characterEventManager.AddListenerToOxygenChangedEvent(oxygenChange);

            UnityAction<SuitModificationType> suitRepair = RepairSuitHandler;
            _characterEventManager.AddListenerToSuitModificationEvent(suitRepair);

            switch (oxygenStateInitial)
            {
                case OxygenState.Stable:
                    var oxygenStableState = new OxygenStableState(this);
                    ChangeOxygenState(oxygenStableState);
                    break;
                case OxygenState.Leaking:
                    var oxygenLeakingState = new OxygenLeakingState(this);
                    ChangeOxygenState(oxygenLeakingState);
                    break;
            }
        }
        void Update()
        {
            _oxygenState.Update();
        }
        public void RepairSuitHandler(SuitModificationType suitModificationType)
        {
            if (suitModificationType == SuitModificationType.FullRepair)
            {
                HealSuitIntegrity(MaxSuitIntegrity);
                ChangeOxygenState(new OxygenStableState(this));
            }
        }

        public IOxygenState GetOxygenState()
        {
            return _oxygenState;
        }

        public void ChangeOxygenState(IOxygenState oxygenState)
        {
            if (_oxygenState != null) _oxygenState.Exit();
            _oxygenState = oxygenState;
            _oxygenState.Enter();
        }


        void OnOxygenChangedHandler(float oxygen, bool isRestored)
        {
            if (currentOxygen <= 0) _characterEventManager.TriggerCharacterDied(characterName);
        }
        public void HealSuitIntegrity(float value)
        {
            currentSuitIntegrity += value;
            if (currentSuitIntegrity > MaxSuitIntegrity) currentSuitIntegrity = MaxSuitIntegrity;
        }

        public void HealOxygen(float value)
        {
            currentOxygen += value;
            if (currentOxygen > MaxOxygen) currentOxygen = MaxOxygen;
            _characterEventManager.TriggerCharacterChangeOxygen(currentOxygen, true);
            Debug.Log("HealOxygen: " + currentOxygen);
        }
    }
}
