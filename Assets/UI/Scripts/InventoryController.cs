using JetBrains.Annotations;
using UnityEngine;

namespace UI.Scripts
{
    public class InventoryController : MonoBehaviour
    {
        [CanBeNull] public GameObject tooltipPrefab;
        public float tooltipOffsetX;
        public float tooltipOffsetY;
    }
}
