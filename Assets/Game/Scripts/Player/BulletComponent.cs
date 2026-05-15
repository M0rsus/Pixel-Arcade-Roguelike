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
        private float _bulletDamage;
        private float _lifeTime;
        private float _range;
        
        private Vector3 _startPosition;

        void Awake()
        {
            GameUpdate.Register(onFixedUpdateListener: this);
            GameUpdate.Register(onUpdateListener: this);
        }

        void Start()
        {
            StartCoroutine(LifeTimeBullet());
            _startPosition = transform.position;
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
            Vector2 move = rigidbodyBullet.transform.up * _bulletSpeed * deltaTime;
            rigidbodyBullet.MovePosition(rigidbodyBullet.position + move);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            string objectTag = collision.gameObject.tag;
            Debug.Log(objectTag);
        }

        public void Initialize(Stats stats)
        {
            _bulletDamage = stats.bulletDamage.GetValue();
            _bulletSpeed = stats.bulletSpeed.GetValue();
            _lifeTime = stats.lifeTime.GetValue();
            _range = stats.range.GetValue();
        }

        private IEnumerator LifeTimeBullet()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}