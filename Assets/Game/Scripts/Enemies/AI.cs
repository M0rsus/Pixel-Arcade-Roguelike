using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    [System.Serializable]
    public abstract class AI
    {
        protected CancellationToken ct;
        protected CancellationToken stuckToken;
        protected CancellationTokenSource cts;
        
        protected Bounds bounds;
        protected Transform playerTransform;
        [SerializeField]
        protected NavMeshAgent agent;
        
        protected State state;
        
        public abstract float Angle { get; set; }

        public void Initialize(CancellationToken ct, CancellationToken stuckCtToken)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.updatePosition = false;
            bounds = LevelContext.Instance.LevelBounds;
            playerTransform = LevelContext.Instance.PlayerTransform;
            this.ct = ct;
            this.stuckToken = stuckCtToken;
            cts = CancellationTokenSource.CreateLinkedTokenSource(this.ct);
        }
        public string GetState()
        {
            return state.ToString();
        }
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);
        public abstract void OnCollisionStay2D(Collision2D collision);
        public abstract void OnCollisionExit2D(Collision2D collision);
        public void ClearCancellationTokenSource()
        {
            if (cts == null) return;
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        protected enum State
        {
            Forward,
            Idle,
            Backward
        }
    }
}