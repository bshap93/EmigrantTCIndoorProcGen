using System;
using Environment.Interactables.Consoles.Scripts.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.Consoles.Scripts
{
    public class OpenableConsole : OpenableObject
    {
        void Start()
        {
            CurrentState = OpenableState.Closed;
            OpenCommand = new OpenConsoleCommand(this);
            CloseCommand = new CloseConsoleCommand(this);
            openingMechanism = OpeningMechanism.UseConsole;
        }
        public override void SetState(OpenableState newState)
        {
            throw new NotImplementedException();
        }
        public override void MoveObject()
        {
            throw new NotImplementedException();
        }
        public override void Open()
        {
            throw new NotImplementedException();
        }
        public override void Close()
        {
            throw new NotImplementedException();
        }
    }
}
