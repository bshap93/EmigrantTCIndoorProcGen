using Environment.Interactables.Triggerables.Scripts.Commands;
using Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class NpcInteractionTriggerable : TriggerableObject
    {
        public GameObject npc;
        ITriggerableCommand _displayDialogueCommand;
        GameObject _player;
        ITriggerableCommand _turnTowardPlayerCommand;
        GameObject _dialogueCanvas;


        void Start()
        {
            isTriggered = false;
            _player = GameObject.FindWithTag("Player");
            _dialogueCanvas = <
        }

        public override void OnTrigger()
        {
            _turnTowardPlayerCommand = new NpcTurnTowardPlayerCommand(
                npc, _player, 0.5f);

            _turnTowardPlayerCommand.Execute();
            
            _displayDialogueCommand = new DisplayDialogueOnCanvas(
                "Hello, I am an NPC. I am here to help you.",
                );
        }
    }
}
