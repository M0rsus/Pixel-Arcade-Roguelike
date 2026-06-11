using System.Threading;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour, IPlayer, IOnUpdateListener, IOnFixedUpdateListener
    {
        [Header("General")]
        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private PlayerInput input;
        
        [Header("Components")]
        [SerializeField]
        private DamageReceiverComponent damageReceiverComponent;
        [SerializeField] 
        private MoveComponent moveComponent;
        [SerializeField] 
        private RotationComponent rotationComponent;
        [SerializeField] 
        private ShootComponent shootComponent;
        [SerializeField]
        private SpawnBulletComponent spawnBulletComponent;
        [SerializeField]
        private ContactComponent contactComponent;
        
        [Header("Parameters")]
        [SerializeField] 
        private Stats stats; 
        
        public IDamageable Damageable => damageReceiverComponent;
        private CancellationToken _ct;

        void Awake()
        {
            GameUpdate.Register(onUpdateListener: this);
            GameUpdate.Register(onFixedUpdateListener: this);
            _ct = CancellationToken.None;
        }

        void OnDisable()
        {
            damageReceiverComponent.OnDisable();
        }

        void OnDestroy()
        {
            GameUpdate.Unregister(onUpdateListener: this);
            GameUpdate.Unregister(onFixedUpdateListener: this);
            damageReceiverComponent.OnDestroy();
        }
        void Start()
        {
            damageReceiverComponent.Initialize(stats, _ct);
            moveComponent.Initialize(rigidBody, input, stats);
            rotationComponent.Initialize(rigidBody, input, stats);
            shootComponent.Initialize(input, spawnBulletComponent, stats);
            contactComponent.Initialize(stats, this);
        }

        public void OnUpdate(float deltaTime)
        {
            shootComponent.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            moveComponent.OnFixedUpdate(deltaTime);
            rotationComponent.OnFixedUpdate(deltaTime);
            contactComponent.OnFixedUpdate(deltaTime);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            contactComponent.OnContact(collision);
        }
    }
}