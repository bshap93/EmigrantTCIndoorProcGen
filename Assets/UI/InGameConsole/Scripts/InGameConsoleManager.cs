using System;
using System.Collections.Generic;
using TMPro;
using UI.Objectives.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameConsole.Scripts
{
    [Serializable]
    public class InGameConsoleManager : MonoBehaviour
    {
        const int MaxMessages = 100; // Limit the number of messages displayed
        public TMP_Text consoleText; // Reference to the UI Text element (or TMP_Text for TextMeshPro)
        public ScrollRect scrollRect; // Reference to the ScrollRect to allow scrolling through messages
        readonly Queue<string> _messageQueue = new(); // Stores messages for the console


        // Method to add a message to the console
        public void LogMessage(string message)
        {
            if (_messageQueue.Count >= MaxMessages) _messageQueue.Dequeue(); // Remove the oldest message if at capacity

            _messageQueue.Enqueue(message); // Add the new message to the queue
            UpdateConsoleText();
        }

        // Update the console text UI with the current messages
        void UpdateConsoleText()
        {
            consoleText.text = string.Join("\n", _messageQueue.ToArray());

            // Optionally scroll to the bottom of the console when new messages are added
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0;
        }

        // Optional method to clear the console
        public void ClearConsole()
        {
            _messageQueue.Clear();
            consoleText.text = string.Empty;
        }
        public void OnObjectiveCompleted(Objective objective)
        {
            throw new NotImplementedException();
        }
        public void OnObjectiveAssigned(Objective objective)
        {
            throw new NotImplementedException();
        }
    }
}
