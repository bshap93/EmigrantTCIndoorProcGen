namespace Characters.Health.Scripts.States.OxygenState
{
    public class OxygenStableState : IOxygenState
    {
        HealthSystem _healthSystem;
        public OxygenStableState(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
        }
        public void Update()
        {
        }
        public void Enter()
        {
        }
        public void Exit()
        {
        }

        public HealthSystem.OxygenState GetState()
        {
            return HealthSystem.OxygenState.Stable;
        }
    }
}
