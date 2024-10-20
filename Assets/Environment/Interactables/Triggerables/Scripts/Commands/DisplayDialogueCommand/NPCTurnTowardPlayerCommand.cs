using DG.Tweening;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand
{
    public class NpcTurnTowardPlayerCommand : ITriggerableCommand
    {
        readonly GameObject _npc; // NPC that will turn
        readonly GameObject _player; // Player that the NPC will turn toward
        readonly float _turnDuration = 0.5f; // Duration of the turn

        public NpcTurnTowardPlayerCommand(GameObject npc, GameObject player, float duration)
        {
            _npc = npc;
            _player = player;
            _turnDuration = duration;
        }

        public void Execute()
        {
            // Calculate direction from NPC to player
            var directionToPlayer = (_player.transform.position - _npc.transform.position).normalized;

            // Calculate the target rotation (Y-axis only, to avoid tilting)
            var lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

            // Smoothly rotate NPC toward the player using DOTween
            _npc.transform.DORotateQuaternion(lookRotation, _turnDuration).SetEase(Ease.InOutQuad);
        }
    }
}
