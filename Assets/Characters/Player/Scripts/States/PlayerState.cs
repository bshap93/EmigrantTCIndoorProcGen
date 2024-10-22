using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Player.Scripts.States
{
    public abstract class PlayerState
    {
        [CanBeNull] protected readonly Transform Target;
        [CanBeNull] PlayerState _formerState;

        protected PlayerState([CanBeNull] Transform target)
        {
            Target = target;
        }
        public abstract void Enter(PlayerCharacter playerCharacter);
        public abstract void Update(PlayerCharacter playerCharacter);
        public abstract void Exit(PlayerCharacter playerCharacter);
    }
}
