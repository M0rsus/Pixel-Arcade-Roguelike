using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    [System.Serializable]
    public abstract class AI
    {
        protected Bounds bounds;
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
            bounds = LevelContext.Instance.LevelBounds;
            playerTransform = LevelContext.Instance.PlayerTransform;
        }
        public string GetState()
        {
            return state.ToString();
        }
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);

        protected enum State
        {
            Forward,
            Idle,
            Backward
        }
    }
}