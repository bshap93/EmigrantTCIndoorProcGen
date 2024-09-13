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


        public float speed = 3.0f;


        Vector3[] _closedPositionsPerPart;

        protected float _currentFramePosition;
        OpenableState _currentState;
        protected ISimpleCommand CloseCommand;
        protected ISimpleCommand OpenCommand;


        // Update is called once per frame


        public abstract void SetState(OpenableState newState);

        public abstract void MoveObject();
    }
}
