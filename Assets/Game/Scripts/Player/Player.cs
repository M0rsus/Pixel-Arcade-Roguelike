using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour, IOnUpdateListener, IOnFixedUpdateListener
    {
        [SerializeField] 
        private Stats stats; 
        [SerializeField] 
        private MoveComponent moveComponent;
        [SerializeField] 
        private ShootComponent shootComponent; 
        [SerializeField]
        private PlayerInput input;
        [SerializeField]
        private SpawnBulletComponent spawnBulletComponent;

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
            moveComponent.Initialize(stats, input);
            shootComponent.Initialize(stats, input, spawnBulletComponent);
        }

        public void OnUpdate(float deltaTime)
        {
            shootComponent.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            moveComponent.OnFixedUpdate(deltaTime);
        }
    }
}