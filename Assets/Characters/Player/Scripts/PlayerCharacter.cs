using System;
using Characters.Enemies.Attacks.Commands;
using Characters.Health.Scripts;
using Characters.Health.Scripts.Commands;
using Characters.Health.Scripts.Debugging;
using Characters.Player.Scripts.States;
using Characters.Scripts;
using Combat.Weapons.Scripts;
using Core.Events;
using Core.Events.EventManagers;
using DunGen;
using Plugins.DunGen.Code;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Player.Scripts
{
    public class PlayerCharacter : MonoBehaviour, IDamageable
    {
        public EditorButtonDealDamage editorButtonDealDamage;
        public NavMeshAgent navMeshAgent;
        public PlayerEventManager playerEventManager;
        [SerializeField] PlayerStateController playerStateController;
        public Weapon currentWeapon;


        [SerializeField] Animator mainPlayerAnimator;

        DungenCharacter _dungenCharacter;
        Transform _initialOrientation;


        public HealthSystem HealthSystem;

        public Transform Position => transform;

        public static PlayerCharacter Instance { get; private set; }

        public float CurrentHealth => HealthSystem.CurrentHealth;


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
            // This must be done before  GameManager
            HealthSystem = new HealthSystem("Player", 100, playerEventManager);
            playerStateController.Initialize(this, new ExploreState(null, mainPlayerAnimator));
            playerEventManager.AddListenerToPlayerTakesDamageEvent(TakeDamage);
            EventManager.ERestartCurrentLevel.AddListener(ResetPlayer);
            playerEventManager.TriggerCharacterStateInitialized();
        }

        void OnDestroy()
        {
            _dungenCharacter.OnTileChanged -= OnCharacterTileChanged;
            playerEventManager.RemoveListenerFromPlayerTakesDamageEvent(TakeDamage);
        }

        // Handle debug damage
        public void TakeDamage(IDamageable damageable, float damage)
        {
            if ((PlayerCharacter)damageable == this)
            {
                var dealDamageCommand = new DealDamageCommand();
                dealDamageCommand.Execute(this, damage, playerEventManager);
            }
        }
        public HealthSystem GetHealthSystem()
        {
            return HealthSystem;
        }
        public void Heal(float value)
        {
            HealthSystem.Heal(value);
        }
        public void StartTurn()
        {
            Debug.Log("Player Turn Started");
        }

        [Button("Reset Player")]
        public void ResetPlayer()
        {
            HealthSystem.CurrentHealth = HealthSystem.MaxHealth;
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
        public Weapon GetCurrentWeapon()
        {
            return currentWeapon;
        }

        public IAttackCommand GetAttackCommand()
        {
            return currentWeapon.GetAttackCommand();
        }
        public void EnterCombatReadyState()
        {
            if (playerStateController.GetCurrentState() is CombatReadyState)
                return;

            playerStateController.ChangeState(
                new CombatReadyState(null, mainPlayerAnimator));
        }
        public void ReturnToExploreState()
        {
            if (playerStateController.GetCurrentState() is ExploreState)
                return;

            playerStateController.ChangeState(new ExploreState(null, mainPlayerAnimator));
        }
        public void PerformAttack()
        {
            if (playerStateController.GetCurrentState() is PlayerAttackingState) return;

            playerStateController.ChangeState(
                new PlayerAttackingState(null, mainPlayerAnimator));
        }
        public bool HasCompletedAction()
        {
            throw new NotImplementedException();
        }
    }
}
