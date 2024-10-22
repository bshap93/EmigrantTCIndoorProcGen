using Characters.Enemies.Attacks.Commands;
using Characters.Health.Scripts.Commands;
using Characters.Health.Scripts.Debugging;
using Characters.Health.Scripts.States;
using Characters.Player.Scripts.States;
using Core.Events;
using Core.Events.EventManagers;
using DunGen;
using Environment.ObjectAttributes.Interfaces;
using Items.Equipment;
using Items.Equipment.Consumables;
using Items.Weapons;
using Items.Weapons.Scripts;
using JetBrains.Annotations;
using Plugins.DunGen.Code;
using Polyperfect.Crafting.Integration;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Characters.Player.Scripts
{
    public class PlayerCharacter : MonoBehaviour, IDamageable
    {
        public EditorButtonDealDamage editorButtonDealDamage;
        public NavMeshAgent navMeshAgent;
        public PlayerEventManager playerEventManager;
        [SerializeField] PlayerStateController playerStateController;
        [FormerlySerializedAs("weaponHandler")] [CanBeNull]
        public EquippableHandler equippableHandler;
        public GameObject hotbar;


        [SerializeField] Animator mainPlayerAnimator;
        public BaseItemObject equippedItem;

        DungenCharacter _dungenCharacter;


        HealthSystem _healthSystem;
        Transform _initialOrientation;

        public Transform Position => transform;

        public static PlayerCharacter Instance { get; private set; }

        public float CurrentHealth => _healthSystem.currentSuitIntegrity;


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
            _initialOrientation = transform;
            _dungenCharacter = GetComponent<DungenCharacter>();
            _dungenCharacter.OnTileChanged += OnCharacterTileChanged;
            _healthSystem = GetComponent<HealthSystem>();
            // This must be done before  GameManager
            playerStateController.Initialize(this, new ExploreState(null, mainPlayerAnimator));
            playerEventManager.AddListenerToPlayerTakesDamageEvent(TakeDamage);
            EventManager.ERestartCurrentLevel.AddListener(ResetPlayer);
            playerEventManager.TriggerCharacterStateInitialized();
            equippableHandler = GetComponentInChildren<EquippableHandler>();
        }

        // Handle debug damage
        public void TakeDamage(IDamageable dmgeable, float damage)
        {
            if ((PlayerCharacter)dmgeable == this)
            {
                var dealDamageCommand = new DealDamageCommand();
                dealDamageCommand.Execute(this, damage, playerEventManager);
            }
        }
        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
        public void Heal(float value)
        {
            _healthSystem.HealSuitIntegrity(value);
        }

        [Button("Reset Player")]
        public void ResetPlayer()
        {
            _healthSystem.currentSuitIntegrity = HealthSystem.MaxSuitIntegrity;
            transform.position = _initialOrientation.position;
            transform.rotation = _initialOrientation.rotation;
        }

        static void OnCharacterTileChanged(DungenCharacter character, Tile previousTile, Tile
            newTile)
        {
            EventManager.EPlayerEnteredRoom.Invoke();
        }

        public void ChangeState(PlayerState newState)
        {
            playerStateController.ChangeState(newState);
        }

        public PlayerState GetCurrentState()
        {
            return playerStateController.GetCurrentState();
        }
        public BaseItemObject GetEquippedItem()
        {
            return equippedItem;
        }

        public IAttackCommand GetAttackCommand()
        {
            if (equippedItem is Weapon weapon) return weapon.GetAttackCommand();

            Debug.LogError("No attack command found");
            return null;
        }
        public void EnterCombatReadyState()
        {
            if (!(playerStateController.GetCurrentState() is RangedCombatReadyState))
                playerStateController.ChangeState(
                    new RangedCombatReadyState(null, mainPlayerAnimator)); // Placeholder for animation
        }


        public void ReturnToExploreState()
        {
            if (!(playerStateController.GetCurrentState() is ExploreState))
                playerStateController.ChangeState(
                    new ExploreState(null, mainPlayerAnimator)); // Placeholder for animation
        }
        public void PerformAttack([CanBeNull] IDamageable target)
        {
            if (equippableHandler != null && equippableHandler is WeaponHandler)
                equippableHandler.Use(target);
        }

        public void UseConsumable()
        {
            if (equippableHandler != null && equippableHandler is ConsumableHandler)
            {
                equippableHandler.Use(this);
                EventManager.EOnItemUsed.Invoke(equippedItem.ID.ToString());
            }
        }

        public void CeaseUsing()
        {
            if (equippableHandler != null)
                equippableHandler.CeaseUsing();
        }
    }
}
