using UnityEngine;

namespace Game
{
    public class BasicEnemy : MonoBehaviour, IOnUpdateListener, IOnFixedUpdateListener
    {
        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private EnemyInput input;
        [SerializeField] 
        private MoveComponent moveComponent;
        [SerializeField] 
        private RotationComponent rotationComponent;
        [SerializeField] 
        private Stats stats; 

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

        void Start()
        {
            moveComponent.Initialize(rigidBody, input, stats);
            rotationComponent.Initialize(rigidBody, input, stats);
        }

        public void OnUpdate(float deltaTime)
        {
            input.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            moveComponent.OnFixedUpdate(deltaTime);
            rotationComponent.OnFixedUpdate(deltaTime);
        }
    }
}