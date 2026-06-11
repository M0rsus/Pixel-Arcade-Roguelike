using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AIChase : AI
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private Transform enemyTransform;
        [SerializeField]
        private float distanceFromPlayer;
        [SerializeField] 
        private bool moveOutsideChaseRange;
        [SerializeField] [Min(0.5f)]
        private float timeBetweenChangeDirection = 0.5f;

        private float _timer = float.MaxValue;
        private Vector3 _localDirection;

        public override float Angle { get; set; }

        public override void OnUpdate(float deltaTime) {}

        public override void OnFixedUpdate(float deltaTime)
        {
            _timer += deltaTime;
            
            float distance = (player.transform.position - enemyTransform.position).sqrMagnitude;

            if (distance < distanceFromPlayer * distanceFromPlayer || distanceFromPlayer == 0)
            {
                state = State.Forward;
                agent.SetDestination(player.transform.position);
                if (!agent.hasPath) return;
            }
            else
            {
                if (moveOutsideChaseRange)
                {
                    state = State.Forward;
                    
                    if (_timer > timeBetweenChangeDirection)
                    {
                        _localDirection = new Vector3(
                            Random.Range(bounds.GetMinX(), bounds.GetMaxX()), 
                            Random.Range(bounds.GetMinY(), bounds.GetMaxY()));
                        
                        agent.SetDestination(_localDirection);
                        _timer = 0;
                    }
                }
                else
                {
                    state = State.Idle;
                    if (agent.hasPath) agent.ResetPath();
                }
            }
            if (agent.hasPath && agent.desiredVelocity.sqrMagnitude > 0.01f)
            {
                Vector2 direction = agent.desiredVelocity.normalized;
                Angle = Vector2.SignedAngle(enemyTransform.up, direction);
            }
            
            agent.nextPosition = enemyTransform.position;
        }
    }
}