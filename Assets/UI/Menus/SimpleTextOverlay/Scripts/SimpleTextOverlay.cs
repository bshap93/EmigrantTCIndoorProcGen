﻿using Core.Events;
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
            button.onClick.AddListener(OnButtonClick);
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
            gameObject.SetActive(true);
        }

        void SetOverlayDead()
        {
            overlayPanel.color = new Color(0, 0, 0, 0.5f);
            overlayText.text = "You Died";
            buttonText.text = "Restart";
            gameObject.SetActive(true);
        }

        public void OnButtonClick()
        {
            if (_currentOverlayState == OverlayState.Paused)
            {
                SetState(OverlayState.Normal);
            }
            else if (_currentOverlayState == OverlayState.Dead)
            {
                SetState(OverlayState.Normal);
                EventManager.ERestartCurrentLevel?.Invoke();
            }
        }
    }
}
