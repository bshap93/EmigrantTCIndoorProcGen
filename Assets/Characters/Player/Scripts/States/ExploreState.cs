using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Player.Scripts.States
{
    public class ExploreState : PlayerState
    {
        static readonly int CombatStance = Animator.StringToHash("RangedCombatStance");
        readonly Animator _animator;
        public ExploreState([CanBeNull] Transform target, Animator animator) : base(
            target)
        {
            _animator = animator;
        }
        public override void Enter(PlayerCharacter playerCharacter)
        {
            _animator.SetBool(CombatStance, false);
        }
        public override void Update(PlayerCharacter playerCharacter)
        {
        }
        public override void Exit(PlayerCharacter playerCharacter)
        {
        }
    }
}
