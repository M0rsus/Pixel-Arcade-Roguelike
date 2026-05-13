using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class BulletComponent : MonoBehaviour, IBulletDirection, IOnFixedUpdateListener
    {
        [SerializeField]
        private float bulletSpeed;
        [SerializeField]
        private float bulletDamage;
        [SerializeField]
        private Rigidbody2D rigidbodyBullet;
        
        public Vector3 BulletDirection {get; set;}

        void Awake()
        {
            GameUpdate.Register(onFixedUpdateListener: this);
        }

        void OnDestroy()
        {
            GameUpdate.Unregister(onFixedUpdateListener: this);
        }
        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 move = rigidbodyBullet.transform.up * bulletSpeed * deltaTime;
            rigidbodyBullet.MovePosition(rigidbodyBullet.position + move);
        }
    }
}