using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class BulletComponent : MonoBehaviour, IOnUpdateListener, IOnFixedUpdateListener
    {
        [SerializeField]
        private Rigidbody2D rigidbodyBullet;
        
        private float _bulletSpeed;
        private int _bulletDamage;
        private float _lifeTime;
        private float _range;
        private float _bounces;
        private bool _canBounceOffEnemies;
        
        private Vector3 _startPosition;
        private Vector2 _direction;

        void Awake()
        {
            GameUpdate.Register(onFixedUpdateListener: this);
            GameUpdate.Register(onUpdateListener: this);
        }

        void Start()
        {
            StartCoroutine(LifeTimeBullet());
            _startPosition = transform.position;
            _direction = rigidbodyBullet.transform.up;
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
            rigidbodyBullet.linearVelocity = move;
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
            rigidbodyBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _bounces--;
        }

        public void Initialize(Stats stats)
        {
            _bulletDamage = stats.bulletDamage.GetValue();
            _bulletSpeed = stats.bulletSpeed.GetValue();
            _lifeTime = stats.lifeTime.GetValue();
            _range = stats.range.GetValue();
            _bounces = stats.bounces.GetValue();
            _canBounceOffEnemies = stats.bounceOffEnemies.GetValue();
        }

        private IEnumerator LifeTimeBullet()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}