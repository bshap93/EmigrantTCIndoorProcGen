using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.ShipSystems.Scripts
{
    public class LevelSystemController : MonoBehaviour
    {
        [FormerlySerializedAs("levelName")] public string floorName;

        public bool initialPower;
        public bool initialAI;
        ShipSystemManager _shipSystemManager;

        void Start()
        {
            _shipSystemManager = FindObjectOfType<ShipSystemManager>();
            // Initialize level status
            _shipSystemManager.InitializeLevel(floorName, initialPower, initialAI); // Example: L1 has power, no AI
        }
        
        

        public void UpdateSystemDependentObjects()
        {
            var power = _shipSystemManager.CheckPower(floorName);
            var ai = _shipSystemManager.CheckAI(floorName);
            
            var objectsOnThisFloor = FindObjectsOfType<MonoBehaviour>()
                .OfType<ISystemDependent>()
                .Where(obj => obj.Floor == floorName);

            foreach (var obj in objectsOnThisFloor)
            {
                obj.UpdateSystemStatus(power, ai);
            }
        }
    }
}
