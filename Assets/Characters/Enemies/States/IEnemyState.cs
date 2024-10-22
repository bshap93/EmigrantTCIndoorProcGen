using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemies.States
{
    public abstract class EnemyState
    {
        [CanBeNull] EnemyState _formerState;
        [CanBeNull] protected Transform Target;
        protected EnemyState([CanBeNull] EnemyState formerState, [CanBeNull] Transform target)
        {
            _formerState = formerState;
            Target = target;
        }
        public abstract void Enter(Enemy enemy);
        public abstract void Update(Enemy enemy);
        public abstract void Exit(Enemy enemy);
    }
}
