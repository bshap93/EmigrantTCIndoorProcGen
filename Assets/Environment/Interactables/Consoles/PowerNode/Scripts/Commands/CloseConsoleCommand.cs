using Core.Utilities.Commands;

namespace Environment.Interactables.Consoles.Scripts.Commands
{
    public class CloseConsoleCommand : ISimpleCommand
    {
        public CloseConsoleCommand(OpenableConsole openableConsole)
        {
            // Debug.Log("CloseConsoleCommand Created");
        }
        public void Execute()
        {
            // Debug.Log("CloseConsoleCommand Execute");
        }
    }
}
