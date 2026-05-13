using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SpawnBulletComponent : MonoBehaviour, IBulletSpawner
    {
        [SerializeField] 
        private Transform bulletSpawner;
        [SerializeField] 
        private GameObject bulletPrefab;

        public void SpawnBullet()
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            bullet.GetComponent<IBulletDirection>().BulletDirection = bulletSpawner.up;
        }
    }
}