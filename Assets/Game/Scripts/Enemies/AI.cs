using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    [System.Serializable]
    public abstract class AI
    {
        [SerializeField]
        protected NavMeshAgent agent;
        
        public abstract float Angle { get; set; }
        public abstract bool IsMoving { get; set; }

        public void Initialize()
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.updatePosition = false;
        }
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);
    }
}