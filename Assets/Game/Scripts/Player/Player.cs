using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour, IOnFixedUpdateListener
    {
        [SerializeField] 
        private MoveRbComponent moveComponent;
        
        [SerializeField]
        private PlayerInput input;

        void Awake()
        {
            GameManager.Register(this);
        }

        void OnDestroy()
        {
            GameManager.Unregister(this);
        }
        void Start()
        {
            moveComponent.Initialize(input);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            moveComponent.OnFixedUpdate(fixedDeltaTime);
        }
    }
}