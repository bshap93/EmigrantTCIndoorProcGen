using UnityEngine;
using UnityEngine.UI;

public class InteractableTooltip : MonoBehaviour
{
    public enum TooltipStatus
    {
        Permit,
        Deny
    }

    public Image SPRBackground;
    // Start is called before the first frame update
    public Image IconBackground;

    public TooltipStatus initalStatus;
    TooltipStatus _currentStatus;
    void Start()
    {
        _currentStatus = initalStatus;
        ChangeColor();
    }
    void ChangeColor()
    {
        switch (_currentStatus)
        {
            case TooltipStatus.Permit:
                SPRBackground.color = Color.blue;
                IconBackground.color = Color.blue;
                break;
            case TooltipStatus.Deny:
                SPRBackground.color = Color.red;
                IconBackground.color = Color.red;
                break;
        }
    }

    public void ChangeStatus(TooltipStatus newStatus)
    {
        _currentStatus = newStatus;
        ChangeColor();
    }
}
