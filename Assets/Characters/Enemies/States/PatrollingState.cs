using UnityEngine;

namespace Characters.Enemies.States
{
    public class PatrollingState : EnemyState
    {
        readonly float _idleTime = 2f;
        public readonly bool ReversedPath;
        int _currentWaypointIndex;
        float _idleTimer;

        bool _isIdle;
        public PatrollingState(EnemyState formerState, bool reversedPath) : base(formerState, null)
        {
            ReversedPath = reversedPath;
        }


        public EnemyState FormerState { get; set; }
        public override void Enter(Enemy enemy)
        {
            if (enemy.waypoints.Count <= 0) enemy.FindWaypoints();
            if (ReversedPath)
                _currentWaypointIndex = enemy.waypoints.Count - 1;
            else
                _currentWaypointIndex = 0;

            enemy.SetEnemyDestination(enemy.waypoints[_currentWaypointIndex].position);
            _isIdle = false;
        }
        public override void Update(Enemy enemy)
        {
            if (!_isIdle)
            {
                if (enemy.HasEnemyReachedDestination() && enemy.waypoints.Count > 0)
                {
                    _isIdle = true;
                    _idleTimer = 0f;
                }
            }
            else
            {
                _idleTimer += Time.deltaTime;

                if (_idleTimer >= _idleTime)
                {
                    if (ReversedPath)
                    {
                        _currentWaypointIndex--;
                        if (_currentWaypointIndex < 0) _currentWaypointIndex = enemy.waypoints.Count - 1;
                    }
                    else
                    {
                        _currentWaypointIndex = (_currentWaypointIndex + 1) % enemy.waypoints.Count;
                    }

                    enemy.SetEnemyDestination(enemy.waypoints[_currentWaypointIndex].position);
                    _isIdle = false;
                }
            }


            if (enemy.CanSeePlayer()) enemy.ChangeState(new ChaseState(this, enemy.player));
        }

        public override void Exit(Enemy enemy)
        {
            // Nothing to do here
        }
    }
}
