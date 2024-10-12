namespace Characters.Scripts
{
    public interface IExtinguishable
    {
        void Extinguish(float sToExtinguish);
        float GetSecondsToExtinguish();
    }
}
