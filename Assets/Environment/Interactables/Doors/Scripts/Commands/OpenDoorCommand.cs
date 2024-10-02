﻿using Core.Utilities.Commands;
using Environment.Interactables.Openable.Scripts;

namespace Environment.Interactables.Doors.Scripts.Commands
{
    public class OpenDoorCommand : ISimpleCommand
    {
        readonly OpenableDoor _openableDoor;

        public OpenDoorCommand(OpenableDoor openableDoor)
        {
            _openableDoor = openableDoor;
        }


        public void Execute()
        {
            _openableDoor.SetState(OpenableObject.OpenableState.Opening);
        }
    }
}