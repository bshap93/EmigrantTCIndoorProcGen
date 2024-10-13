using System.Collections.Generic;
using Core.Utilities.Commands;
using UnityEngine;

namespace Environment.Interactables.Openable.Scripts
{
    public abstract class OpenableObject : MonoBehaviour, IOpenable
    {
        public enum OpenableState
        {
            Open,
            Closed,
            Opening,
            Closing
        }

        public enum OpenerAgent
        {
            Player,
            Enemy
        }

        public enum OpeningMechanism
        {
            Button,
            Proximity,
            UseCraftingStation,
            UseConsole
        }

        public List<OpenerAgent> agentsAllowedToOpen;

        public float speed = 3.0f;
        
        public bool openable = true;


        Vector3[] _closedPositionsPerPart;
        protected ISimpleCommand CloseCommand;

        protected float CurrentFramePosition;
        protected OpenableState CurrentState;
        protected ISimpleCommand OpenCommand;

        protected OpeningMechanism openingMechanism;

        public bool IsOpen => CurrentState == OpenableState.Open || CurrentState == OpenableState.Opening;

        public bool IsClosed => CurrentState == OpenableState.Closed || CurrentState == OpenableState.Closing;


        // Update is called once per frame


        public abstract void SetState(OpenableState newState);

        public abstract void MoveObject();

        public abstract void Open();

        public abstract void Close();
    }
}
