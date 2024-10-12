public interface IUsableItem
{
    bool IsTwoHanded { get; }
    void Equip();
    void Use();
    void Unequip();
}
