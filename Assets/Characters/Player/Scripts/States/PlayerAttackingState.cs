using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Player.Scripts.States
{
    public class PlayerAttackingState : PlayerState
    {
        static readonly int CombatStance = Animator.StringToHash("RangedCombatStance");
        readonly Animator _animator;
        public PlayerAttackingState([CanBeNull] Transform target, Animator animator)
            : base(target)
        {
            _animator = animator;
        }

        public override void Enter(PlayerCharacter playerCharacter)
        {
            _animator.SetBool(CombatStance, true);
            Debug.Log("Player is attacking");
        }

        public override void Update(PlayerCharacter playerCharacter)
        {
            // Attacking typically doesn't need to handle updates unless you have complex animations
        }

        public override void Exit(PlayerCharacter playerCharacter)
        {
            // Clean up any attack state-specific logic
        }
    }
}
