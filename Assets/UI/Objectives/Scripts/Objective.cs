﻿using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Objectives.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "Objectives", order = 1)]
    public class Objective : ScriptableObject
    {
        [FormerlySerializedAs("IsCompleted")] public bool isCompleted;
        [FormerlySerializedAs("ObjectiveText")]
        public bool isActive;
        public string objectiveText;


        public Objective(string objectiveText)
        {
            this.objectiveText = objectiveText;
            isCompleted = false;
        }
    }
}