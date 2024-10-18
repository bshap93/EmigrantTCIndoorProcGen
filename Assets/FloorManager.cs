using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public enum Floor
    {
        L1,
        L2,
        L3,
        L4,
        L5
    }

    public bool isElectricictyOn;
    public bool isOxygenOn;
    public Floor currentFloor;
}
