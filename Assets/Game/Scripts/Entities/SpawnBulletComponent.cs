using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SpawnBulletComponent : MonoBehaviour, IBulletSpawner
    {
        private Stats _stats;
        [SerializeField] 
        private Transform bulletSpawner;
        [SerializeField] 
        private GameObject bulletPrefab;

        public void Initialize(Stats stats)
        {
            _stats = stats;
        }
        public void SpawnBullet()
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            var bulletComponent = bullet.GetComponent<BulletComponent>();
            bulletComponent.Initialize(_stats, GetComponentInParent<Collider2D>());
        }
    }
}