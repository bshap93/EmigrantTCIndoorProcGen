using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ShipSystems.Scripts
{
    public class ShipSystemManager : MonoBehaviour
    {
        public Dictionary<string, LevelSystems> levelSystemStatus = new();


        public void InitializeLevel(string levelName, bool initialPower, bool initialAI)
        {
            levelSystemStatus[levelName] = new LevelSystems { hasPower = initialPower, hasAI = initialAI };
        }

        public bool CheckPower(string levelName)
        {
            return levelSystemStatus[levelName].hasPower;
        }
        public bool CheckAI(string levelName)
        {
            return levelSystemStatus[levelName].hasAI;
        }

        public void SetPowerStatus(string levelName, bool status)
        {
            levelSystemStatus[levelName].hasPower = status;
            // Trigger events or updates based on power change
        }

        public void SetAIStatus(string levelName, bool status)
        {
            levelSystemStatus[levelName].hasAI = status;
            // Trigger events or updates based on AI change
        }

        [Serializable]
        public class LevelSystems
        {
            public bool hasPower;
            public bool hasAI;
        }
    }
}
