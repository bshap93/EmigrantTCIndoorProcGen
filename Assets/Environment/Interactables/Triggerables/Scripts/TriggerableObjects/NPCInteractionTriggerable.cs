using Environment.Interactables.Triggerables.Scripts.Commands;
using Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Interactables.Triggerables.Scripts.TriggerableObjects
{
    public class NpcInteractionTriggerable : TriggerableObject
    {
        public GameObject npc;
        [FormerlySerializedAs("_dialogueCanvas")]
        public GameObject dialogueCanvas;
        public TMP_Text dialogueText;
        public float timeToHide = 7f;
        ITriggerableCommand _displayDialogueCommand;
        ITriggerableCommand _hideDialogueCommand;
        GameObject _player;
        ITriggerableCommand _turnTowardPlayerCommand;


        void Start()
        {
            isTriggered = false;
            _player = GameObject.FindWithTag("Player");
        }


        public override void OnTrigger()
        {
            _turnTowardPlayerCommand = new NpcTurnTowardPlayerCommand(
                npc, _player, 0.5f);

            _turnTowardPlayerCommand.Execute();

            _displayDialogueCommand = new DisplayDialogueOnCanvas(
                "Please, restore power to this level of the ship. The code is 342444.",
                dialogueText, dialogueCanvas);

            _displayDialogueCommand.Execute();

            _hideDialogueCommand = new HideDialogueOnCanavas(dialogueCanvas, timeToHide);

            _hideDialogueCommand.Execute();
        }
    }
}
