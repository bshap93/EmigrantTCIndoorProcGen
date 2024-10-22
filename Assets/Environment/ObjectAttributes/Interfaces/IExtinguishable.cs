namespace Environment.ObjectAttributes.Interfaces
{
    public interface IExtinguishable
    {
        void Extinguish(float sToExtinguish);
        float GetSecondsToExtinguish();
    }
}
