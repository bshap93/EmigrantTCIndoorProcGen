using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Menus.SimpleTextOverlay.Scripts
{
    public enum OverlayState
    {
        Paused,
        Dead,
        Normal
    }

    public class SimpleTextOverlay : MonoBehaviour
    {
        [FormerlySerializedAs("_overlayPanel")]
        public Image overlayPanel;
        public TMP_Text overlayText;
        public Button button;
        public TMP_Text buttonText;
        OverlayState _currentOverlayState;


        void Start()
        {
            _currentOverlayState = OverlayState.Normal;
            button = GetComponentInChildren<Button>();
            buttonText = button.GetComponentInChildren<TMP_Text>();
        }

        public void OnButtonClick()
        {
            switch (_currentOverlayState)
            {
                case OverlayState.Paused:
                    EventManager.EResumeGame?.Invoke();
                    break;
                case OverlayState.Dead:
                    Debug.Log("Restarting Game");
                    break;
            }
        }

        public void SetState(OverlayState overlayState)
        {
            switch (overlayState)
            {
                case OverlayState.Normal:
                    gameObject.SetActive(false);
                    break;
                case OverlayState.Paused:
                    SetOverlayPause();
                    break;
                case OverlayState.Dead:
                    SetOverlayDead();

                    break;
            }
        }

        void SetOverlayPause()
        {
            overlayPanel.color = new Color(0, 0, 0, 0.5f);
            overlayText.text = "Paused";
            buttonText.text = "Resume";
            _currentOverlayState = OverlayState.Paused;
            gameObject.SetActive(true);
        }

        void SetOverlayDead()
        {
            overlayPanel.color = new Color(0, 0, 0, 0.5f);
            overlayText.text = "You Died";
            buttonText.text = "Restart";
            _currentOverlayState = OverlayState.Dead;
            gameObject.SetActive(true);
        }
    }
}
