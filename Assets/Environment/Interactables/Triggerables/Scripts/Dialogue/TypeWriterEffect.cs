using System.Collections;
using TMPro;
using UnityEngine;

namespace Environment.Interactables.Triggerables.Scripts.Dialogue
{
    // Needed for TextMeshPro

    public class TypewriterEffect : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText; // Drag your TextMeshProUGUI here in the Inspector
        public float typingSpeed = 0.05f; // Delay between each letter
        string currentText = ""; // The current text being displayed

        string fullText; // The complete dialogue text

        // Use this method to start the typewriter effect
        public void StartTypewriter(string dialogue)
        {
            fullText = dialogue;
            StartCoroutine(TypeText());
        }

        IEnumerator TypeText()
        {
            for (var i = 0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i + 1); // Add one letter at a time
                dialogueText.text = currentText;
                yield return new WaitForSeconds(typingSpeed); // Wait for the typing speed interval
            }
        }
    }
}
