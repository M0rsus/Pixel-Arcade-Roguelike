using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [Serializable]
    public class AIChase : AI
    {
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
        private bool _timerIsRunning;
        private bool _isStuckSituation;

        public override float Angle { get; set; }

        public override void OnUpdate(float deltaTime) {}

        public override void OnFixedUpdate(float deltaTime)
        {
            _timer += deltaTime;
            float distance = float.MaxValue;

            if (playerTransform != null)
                distance = (playerTransform.position - enemyTransform.position).sqrMagnitude;

            if ((distance < distanceFromPlayer * distanceFromPlayer || distanceFromPlayer == 0)
                && playerTransform != null)
            {
                state = State.Forward;
                agent.SetDestination(playerTransform.position);
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
                            Random.Range(Bounds.Instance.GetMinX(), Bounds.Instance.GetMaxX()), 
                            Random.Range(Bounds.Instance.GetMinY(), Bounds.Instance.GetMaxY()));
                        
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

            if (_isStuckSituation)
            {
                state = State.Backward;
            }
            agent.nextPosition = enemyTransform.position;
        }

        public override void OnCollisionStay2D(Collision2D collision)
        {
            if (_timerIsRunning) return;
            
            ClearCancellationTokenSource();
            cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            Sticking().Forget();
        }

        public override void OnCollisionExit2D(Collision2D collision)
        {
            ClearCancellationTokenSource();
        }

        private async UniTaskVoid Sticking()
        {
            try
            {
                _timerIsRunning = true;

                await UniTask.Delay(
                    TimeSpan.FromSeconds(2.5f),
                    ignoreTimeScale: false,
                    cancellationToken: cts.Token);

                _isStuckSituation = true;

                await UniTask.Delay(
                    TimeSpan.FromSeconds(0.4f),
                    ignoreTimeScale: false,
                    cancellationToken: stuckToken);
            }
            catch (OperationCanceledException) { }
            finally
            {
                _isStuckSituation = false; 
                _timerIsRunning = false;
            }
        }

        protected override void PlayerDestroyed()
        {
            moveOutsideChaseRange = true;
        }
    }
}