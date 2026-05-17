using UnityEngine;

namespace Game
{
    public class BasicEnemy : MonoBehaviour, IOnUpdateListener, IOnFixedUpdateListener
    {
        [SerializeField] 
        private Stats stats; 
        [SerializeField]
        private MoveComponent moveComponent;
        [SerializeField]
        private AIChase ai;

        void Awake()
        {
            GameUpdate.Register(onUpdateListener: this);
            GameUpdate.Register(onFixedUpdateListener: this);
        }

        void OnDestroy()
        {
            GameUpdate.Unregister(onUpdateListener: this);
            GameUpdate.Unregister(onFixedUpdateListener: this);
        }

        public void OnUpdate(float deltaTime)
        {
            ai.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            
        }
    }
}