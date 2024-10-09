using Core.Events.EventManagers;
using Core.SaveSystem.Scripts;
using Items.Inventory.Scripts;
using JetBrains.Annotations;
using UI.ETCCustomCursor.Scripts.Commands;
using UI.InGameConsole.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Core.GameManager.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject consoleManagerObject;


        public UnityEvent<string> onSystemActivated;

        public GameObject player;

        [CanBeNull] public SaveManager saveManager;

        public InGameConsoleManager inGameConsoleManager;

        public ItemWorldFragmentManager itemWorldFragmentManager;
        DisableCursorCommand _disableCursorCommand;

        EnableFreeCursorCommand _enableFreeCursorCommand;

        PlayerEventManager _playerEventManager;
        public static GameManager Instance { get; private set; }


        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        void Start()
        {
            _playerEventManager = player.GetComponent<PlayerEventManager>();


            if (itemWorldFragmentManager == null)
                // It's a child of the GameManager object
                itemWorldFragmentManager = GetComponentInChildren<ItemWorldFragmentManager>();


            // _disableCursorCommand = new DisableCursorCommand();
            _enableFreeCursorCommand = new EnableFreeCursorCommand();

            // _disableCursorCommand.Execute();


            if (saveManager != null) saveManager.InitializedDungeonLevel(null);
        }
    }
}
