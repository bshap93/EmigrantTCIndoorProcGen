using Characters.Health.Scripts.States;
using Characters.Health.Scripts.States.OxygenState;
using Characters.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OxygenStatusIndicator : MonoBehaviour
{
    [FormerlySerializedAs("_oxygenStatusImage")]
    public Image oxygenStatusImage;
    HealthSystem _healthSystem;
    Color _regularColor;
    // Start is called before the first frame update
    void Start()
    {
        _healthSystem = PlayerCharacter.Instance.GetHealthSystem(); // Get the player's health system
        _regularColor = oxygenStatusImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthSystem.GetOxygenState() is OxygenLeakingState)
            oxygenStatusImage.color = Color.red;
        else
            oxygenStatusImage.color = _regularColor;
    }
}
