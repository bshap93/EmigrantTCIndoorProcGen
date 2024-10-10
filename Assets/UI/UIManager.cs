using Characters.Health.Scripts.States;
using Core.Events;
using Core.Events.EventManagers;
using Core.GameManager.Scripts.Commands;
using UI.ETCCustomCursor.Scripts;
using UI.Health.Scripts;
using UI.InGameConsole.Scripts;
using UI.Menus.SimpleTextOverlay.Scripts;
using UI.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        static readonly int Hit = Animator.StringToHash("Hit");
        static readonly int OxygenRestored = Animator.StringToHash("OxygenRestored");
        [SerializeField] GameObject simpleTextOverlayGameObject;
        public string cursorName;
        public Vector2 cursorHotspot;
        [FormerlySerializedAs("healthBarUI")] [FormerlySerializedAs("statsBarUI")]
        public SuitIntegrityHealthBarUI suitIntegrityHealthBarUI;
        public OxygenBarUI oxygenBarUI;

        public InGameConsoleManager inGameConsoleManager;

        public GameObject statusEffectOverlay;

        public Canvas uiCanvas;

        HealthSystem _healthSystem;

        GameObject _player;

        PlayerEventManager _playerEventManager;
        VignetteController _vignetteController;


        public static UIManager Instance { get; private set; }

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
            if (simpleTextOverlayGameObject == null)
                simpleTextOverlayGameObject.GetComponent<SimpleTextOverlay>();

            // Create and set the custom cursor
            new CustomCursor(cursorName, cursorHotspot);

            if (_player == null) _player = GameObject.FindWithTag("Player");

            _healthSystem = _player.GetComponent<HealthSystem>();
            _playerEventManager = _player.GetComponent<PlayerEventManager>();


            EventManager.EResumeGame.AddListener(OnResumeGame);
            EventManager.EPauseGame.AddListener(OnPauseGame);

            UnityAction<float, bool> healthChange = OnHealthChanged;
            _playerEventManager.AddListenerToHealthChangedEvent(healthChange);

            UnityAction<float, bool> oxygenChange = OnOxygenChanged;
            _playerEventManager.AddListenerToOxygenChangedEvent(oxygenChange);

            UnityAction<string> dead = OnDead;
            _playerEventManager.AddListenerToCharacterEvent(dead);

            _vignetteController = GetComponentInChildren<VignetteController>();


            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDestroy()
        {
            EventManager.EResumeGame.RemoveListener(OnResumeGame);
            EventManager.EPauseGame.RemoveListener(OnPauseGame);

            UnityAction<float, bool> healthChange = OnHealthChanged;
            _playerEventManager.RemoveListenerFromSuitIntegrityChange(healthChange);

            UnityAction<float, bool> oxygenChange = OnOxygenChanged;
            _playerEventManager.RemoveListenerFromOxygenChange(oxygenChange);

            UnityAction<string> dead = OnDead;
            _playerEventManager.RemoveListenerFromCharacterEvent(dead);

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnLoadNewScene();
        }

        void OnLoadNewScene()
        {
            if (_playerEventManager == null) _playerEventManager = _player.GetComponent<PlayerEventManager>();

            if (_healthSystem == null) _healthSystem = _player.GetComponent<HealthSystem>();
        }


        void OnHealthChanged(float health, bool damage)
        {
            // If it's decreasing, play the animation
            if (damage) _vignetteController.ShowDamageVignette();
            suitIntegrityHealthBarUI.UpdateSuitIntegrityBar(health);
        }

        void OnOxygenChanged(float oxygen, bool isRestored)
        {
            if (isRestored) _vignetteController.ShowHealOxygenVignette();
            oxygenBarUI.UpdateOxygenBar(oxygen);
        }


        void OnPauseGame()
        {
            simpleTextOverlayGameObject.GetComponent<SimpleTextOverlay>()
                .SetState(OverlayState.Paused);
        }

        void OnResumeGame()
        {
            simpleTextOverlayGameObject.SetActive(false);
        }

        void OnDead(string character)
        {
            if (character != "Player") return;
            var playerDieCommand = new PlayerDieCommand();
            playerDieCommand.Execute();
            simpleTextOverlayGameObject.GetComponent<SimpleTextOverlay>()
                .SetState(OverlayState.Dead);
        }
    }
}
