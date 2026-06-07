using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    [System.Serializable]
    public abstract class AI
    {
        [SerializeField]
        protected NavMeshAgent agent;
        
        protected State state;
        
        public abstract float Angle { get; set; }

        public void Initialize()
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.updatePosition = false;
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