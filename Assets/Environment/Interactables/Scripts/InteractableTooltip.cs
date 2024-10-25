using UnityEngine;
using UnityEngine.UI;

namespace Environment.Interactables.Scripts
{
    public class InteractableTooltip : MonoBehaviour
    {
        public enum TooltipStatus
        {
            Permit,
            Deny
        }

        public Image SprBackground;
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
                    SprBackground.color = Color.blue;
                    IconBackground.color = Color.blue;
                    break;
                case TooltipStatus.Deny:
                    SprBackground.color = Color.red;
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
}
