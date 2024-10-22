namespace Environment.Interactables.Collectables.Scripts
{
    public interface ICollectable
    {
        void CollectObject();

        public void SetState(CollectableObject.CollectableState state);
    }
}
