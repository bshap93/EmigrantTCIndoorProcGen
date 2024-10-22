namespace Characters.Health.Scripts.States.OxygenState
{
    public interface IOxygenState
    {
        HealthSystem.OxygenState GetState();
        void Update();
        void Enter();
        void Exit();
    }
}
