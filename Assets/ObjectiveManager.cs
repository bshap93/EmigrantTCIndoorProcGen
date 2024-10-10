using System.Collections.Generic;
using UI.Objectives.Scripts;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives;
    public ObjectiveUI objectiveUI;
    void Start()
    {
        objectiveUI.OnObjectiveAssigned(objectives[0]);
    }

    public void CompleteCurrentObjective()
    {
        objectives[0].isCompleted = true;
        objectiveUI.OnObjectiveCompleted(objectives[0]);
    }
}
