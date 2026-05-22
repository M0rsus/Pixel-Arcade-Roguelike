using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AIChase : AI
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Transform enemyTransform;
        [SerializeField]
        private float distanceFromPlayer;
        [SerializeField] 
        private bool moveOutsideChaseRange;
        [SerializeField] [Min(0.5f)]
        private float timeBetweenChangeDirection = 0.5f;

        private float _timer;
        private Vector3 _localDirection;

        public override float Angle { get; set; }
        public override bool IsMoving { get; set; }

        public override void OnUpdate(float deltaTime)
        {
            agent.SetDestination(playerTransform.position);
            /*IsMoving = false;
            
            Vector2 direction = playerTransform.position - enemyTransform.position;
            float distance = (playerTransform.position - enemyTransform.position).sqrMagnitude;

            if (distance < distanceFromPlayer * distanceFromPlayer || distanceFromPlayer == 0)
            {
                IsMoving = true;
            }
            else if (moveOutsideChaseRange)
            {
                IsMoving = true;
                _timer += deltaTime;
                direction = _localDirection - enemyTransform.position;
                
                if (_timer > timeBetweenChangeDirection)
                {
                    _localDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f)) - enemyTransform.position;
                    _timer = 0;
                }
            }
            Angle = Vector2.SignedAngle(enemyTransform.up, direction);*/
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            if (!agent.hasPath) return;
            
            IsMoving = true;
            
            Vector2 direction = agent.desiredVelocity.normalized;
            
            //Vector2 direction = (agent.steeringTarget - enemyTransform.position).normalized;
            
            Angle = Vector2.SignedAngle(enemyTransform.up, direction);
            
            agent.nextPosition = enemyTransform.position;
        }
    }
}