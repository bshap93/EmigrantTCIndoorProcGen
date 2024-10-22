using System.Collections.Generic;
using Characters.Player.Scripts;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand
{
    public class HideDialogueOnCanavas : ITriggerableCommand
    {
        readonly GameObject _dialogueCanvas;
        readonly float _timeToHide;

        public HideDialogueOnCanavas(GameObject dialogueCanvas, float timeToHide)
        {
            _dialogueCanvas = dialogueCanvas;
            _timeToHide = timeToHide;
        }


        public void Execute()
        {
            var hideDialogue = HideDialogue();
            PlayerCharacter.Instance.StartCoroutine(hideDialogue);
        }

        IEnumerator<WaitForSeconds> HideDialogue()
        {
            yield return new WaitForSeconds(_timeToHide);
            _dialogueCanvas.SetActive(false);
        }
    }
}
