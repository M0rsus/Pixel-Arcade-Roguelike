using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class BulletComponent : MonoBehaviour, IOnUpdateListener, IOnFixedUpdateListener
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField] 
        private float speedDistanceFactor;
        private Collider2D _bulletCollider;
        
        private float _bulletSpeed;
        private int _bulletDamage;
        private float _lifeTime;
        private float _range;
        private float _bounces;
        private bool _canBounceOffEnemies;
        
        private Vector3 _startPosition;
        private Vector2 _direction;
        private Collider2D _ownerCollider;
        private bool _ownerCollisionEnabled;
        private float _requiredDistance;

        void Awake()
        {
            GameUpdate.Register(onFixedUpdateListener: this);
            GameUpdate.Register(onUpdateListener: this);
        }

        void Start()
        {
            StartCoroutine(LifeTimeBullet());
            _startPosition = transform.position;
            _direction = rb.transform.up;
        }

        void OnDestroy()
        {
            GameUpdate.Unregister(onFixedUpdateListener: this);
            GameUpdate.Unregister(onUpdateListener: this);
        }
        public void OnUpdate(float deltaTime)
        {
            float distance = (transform.position - _startPosition).sqrMagnitude;
            if (distance > _range * _range)
                Destroy(gameObject);
        }
        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 move = _direction * _bulletSpeed;
            rb.linearVelocity = move;
            
            if (_ownerCollisionEnabled) return;
            float travelled = Vector2.Distance(rb.position, _startPosition);
            
            if (travelled < _requiredDistance) return;
            Physics2D.IgnoreCollision(_bulletCollider, _ownerCollider, false);
            _ownerCollisionEnabled = true;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            bool isEntity = collision.gameObject.TryGetComponent<IEntity>(out var component);
            if (isEntity)
                component.Damageable.TakeDamage(_bulletDamage);
            
            if ((!_canBounceOffEnemies && isEntity) || _bounces <= 0)
            {
                Destroy(gameObject);
                return;
            }
            Vector2 normal = collision.contacts[0].normal;
            _direction = Vector2.Reflect(_direction, normal).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            rb.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _bounces--;
            
            if (_ownerCollisionEnabled) return;
            Physics2D.IgnoreCollision(_bulletCollider, _ownerCollider, false);
            _ownerCollisionEnabled = true;
        }

        public void Initialize(Stats stats, Collider2D ownerCollider)
        {
            _bulletDamage = stats.bulletDamage.GetValue();
            _bulletSpeed = stats.bulletSpeed.GetValue();
            _lifeTime = stats.lifeTime.GetValue();
            _range = stats.range.GetValue();
            _bounces = stats.bounces.GetValue();
            _canBounceOffEnemies = stats.bounceOffEnemies.GetValue();
            
            _bulletCollider = GetComponent<Collider2D>();
            _ownerCollider = ownerCollider;
            Physics2D.IgnoreCollision(_bulletCollider, _ownerCollider, true);
            _requiredDistance = _bulletSpeed * speedDistanceFactor;
        }

        private IEnumerator LifeTimeBullet()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}