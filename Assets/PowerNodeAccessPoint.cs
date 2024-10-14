using System.Collections.Generic;
using System.Linq;
using Core.ShipSystems.Scripts;
using Environment.Interactables.Openable.Scripts;
using UnityEngine;

public class PowerNodeAccessPoint : OpenableObject
{
    public GameObject powerNodeCanvas;
    List<GameObject> _aiOffIcons;
    List<GameObject> _aiOnIcons;
    List<string> _levelNames = new();

    List<GameObject> _powerOffIcons;

    List<GameObject> _powerOnIcons;
    ShipSystemManager _shipSystemManager;

    // Start is called before the first frame update
    void Start()
    {
        _shipSystemManager = FindObjectOfType<ShipSystemManager>();
        _levelNames = _shipSystemManager.levelSystemStatus.Keys.ToList();

        _powerOnIcons = GameObject.FindGameObjectsWithTag("PowerOnIcon").ToList();
        _aiOnIcons = GameObject.FindGameObjectsWithTag("AiOnIcon").ToList();
        _aiOffIcons = GameObject.FindGameObjectsWithTag("AiOffIcon").ToList();
        _powerOffIcons = GameObject.FindGameObjectsWithTag("PowerOffIcon").ToList();

        UpdateUIIcons();
    }

    void UpdateUIIcons()
    {
        foreach (var levelName in _levelNames)
        {
            var power = _shipSystemManager.CheckPower(levelName);
            var ai = _shipSystemManager.CheckAI(levelName);
        }
    }

    public override void SetState(OpenableState newState)
    {
    }
    public override void MoveObject()
    {
    }
    public override void Open()
    {
    }
    public override void Close()
    {
    }
}
