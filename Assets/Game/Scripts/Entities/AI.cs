using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    [System.Serializable]
    public abstract class AI
    {
        protected CancellationToken _ct;
        protected CancellationToken _stuckCt;
        protected CancellationTokenSource _cts;
        protected CancellationTokenSource _stuckCts;
        
        protected Transform playerTransform;
        [SerializeField]
        protected NavMeshAgent agent;
        
        protected State state;
        
        public abstract float Angle { get; set; }

        public void Initialize()
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.updatePosition = false;
            
            playerTransform = LevelContext.Instance.PlayerTransform;
            Player.Destroyed += PlayerDestroyed;
            
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _stuckCts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            _stuckCt =  _stuckCts.Token;
        }
        public string GetState()
        {
            return state.ToString();
        }
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);
        public abstract void OnCollisionStay2D(Collision2D collision);
        public abstract void OnCollisionExit2D(Collision2D collision);

        public void OnDestroy()
        {
            Player.Destroyed -= PlayerDestroyed;
            if (_stuckCts == null) return;
            _stuckCts.Cancel();
            _stuckCts.Dispose();
            _stuckCts = null;
        }
        public void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        protected virtual void PlayerDestroyed() { }

        protected enum State
        {
            Forward,
            Idle,
            Backward
        }
    }
}