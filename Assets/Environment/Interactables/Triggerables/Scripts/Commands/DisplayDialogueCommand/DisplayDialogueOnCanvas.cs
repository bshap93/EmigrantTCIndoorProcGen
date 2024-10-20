using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Commands.DisplayDialogueCommand
{
    public class DisplayDialogueOnCanvas : ITriggerableCommand
    {
        string _dialogue;
        GameObject _dialogueCanvas;


        public DisplayDialogueOnCanvas(string dialogue, GameObject dialogueCanvas)
        {
            _dialogue = dialogue;
            _dialogueCanvas = dialogueCanvas;
        }


        public void Execute()
        {
        }
    }
}
