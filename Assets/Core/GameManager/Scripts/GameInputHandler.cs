using Core.Events;
using Core.GameManager.Scripts.Commands;
using Core.InputHandler.Scripts;
using UnityEngine;

namespace Core.GameManager.Scripts
{
    public class GameInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] bool isPaused;
        PauseGameCommand _pauseGameCommand;
        ResumeGameCommand _resumeGameCommand;


        void Start()
        {
            isPaused = false;
            _pauseGameCommand = new PauseGameCommand();
            _resumeGameCommand = new ResumeGameCommand();

            EventManager.EPauseGame.AddListener(OnPauseGame);
            EventManager.EResumeGame.AddListener(OnResumeGame);
        }

        void Update()
        {
            HandleInput();
        }
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    EventManager.EResumeGame?.Invoke();
                else
                    EventManager.EPauseGame?.Invoke();
            }
        }


        void OnPauseGame()
        {
            _pauseGameCommand.Execute();
            isPaused = true;
        }

        void OnResumeGame()
        {
            _resumeGameCommand.Execute();
            isPaused = false;
        }
    }
}
