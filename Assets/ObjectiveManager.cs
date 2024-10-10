using System.Collections.Generic;
using UI.Objectives.Scripts;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives;
    public ObjectiveUI objectiveUI;
    Objective currentObjective;
    void Start()
    {
        objectiveUI.OnObjectiveAssigned(objectives[0]);
        currentObjective = objectives[0];
    }

    public void CompleteCurrentObjective()
    {
        currentObjective.isCompleted = true;
        objectiveUI.OnObjectiveCompleted(currentObjective);
    }
    public object GetCurrentObjective()
    {
        return currentObjective;
    }
}
