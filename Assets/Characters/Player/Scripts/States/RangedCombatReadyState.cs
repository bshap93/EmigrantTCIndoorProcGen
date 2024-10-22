using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Player.Scripts.States
{
    public class RangedCombatReadyState : PlayerState
    {
        static readonly int RangedCombatStance = Animator.StringToHash("RangedCombatStance");
        readonly Animator _animator;
        PlayerCharacter _playerCharacter;
        public RangedCombatReadyState([CanBeNull] Transform target, Animator animator) : base(
            target)
        {
            _animator = animator;
        }
        public override void Enter(PlayerCharacter playerCharacter)
        {
            // Player is now ready for combat
            _animator.SetBool(RangedCombatStance, true);
        }
        public override void Update(PlayerCharacter playerCharacter)
        {
        }
        public override void Exit(PlayerCharacter playerCharacter)
        {
            _animator.SetBool(RangedCombatStance, false);
        }
    }
}
