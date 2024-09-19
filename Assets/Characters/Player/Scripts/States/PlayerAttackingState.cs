﻿using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Player.Scripts.States
{
    public class PlayerAttackingState : PlayerState
    {
        static readonly int CombatStance = Animator.StringToHash("CombatStance");
        readonly Animator _animator;
        public PlayerAttackingState([CanBeNull] Transform target, Animator animator)
            : base(target)
        {
            _animator = animator;
        }

        public override void Enter(PlayerCharacter playerCharacter)
        {
            _animator.SetBool(CombatStance, true);

            // Execute the attack command (could be melee or ranged)
            var attackCommand = playerCharacter.GetAttackCommand();
            attackCommand.Execute(
                null,
                playerCharacter.GetCurrentWeapon().GetDamage());

            Debug.Log("Player is attacking with " + playerCharacter.GetCurrentWeapon().name);
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