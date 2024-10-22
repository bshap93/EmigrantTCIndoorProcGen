using Environment.Interactables.Triggerables.Scripts.Dialogue;
using TMPro;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand
{
    public class DisplayDialogueOnCanvas : ITriggerableCommand
    {
        readonly string _dialogue;
        readonly TMP_Text _dialogueText;
        readonly GameObject _dialogueCanvas;


        public DisplayDialogueOnCanvas(string dialogue, TMP_Text dialogueText, GameObject dialogueCanvas)
        {
            _dialogueCanvas = dialogueCanvas;
            _dialogue = dialogue;
            _dialogueText = dialogueText;
        }


        public void Execute()
        {
            _dialogueCanvas.SetActive(true);
            if (_dialogueText != null) _dialogueText.text = _dialogue;
            _dialogueText.gameObject.GetComponent<TypewriterEffect>().StartTypewriter(_dialogue);
        }
    }
}
