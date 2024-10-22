namespace Core.ShipSystems.Scripts
{
    public interface ISystemDependent
    {
        string Floor { get; }
        void UpdateSystemStatus(bool hasPower, bool hasAI);
    }
}
