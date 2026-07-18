using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SpawnBulletComponent : MonoBehaviour, IBulletSpawner
    {
        private Stats _stats;
        [SerializeField] 
        private GameObject bulletPrefab;

        [SerializeField] 
        private float spawnDistance;

        public void Initialize(Stats stats)
        {
            _stats = stats;
        }
        public void SpawnBullet()
        {
            var spawnedBullets = new List<BulletComponent>();
            int bullets = _stats.bullets.GetValue();
            
            if (bullets <= 1)
            {
                Quaternion bulletRotation = GetBulletRotation();
                var bullet = InitializeBullet(bulletRotation);
                spawnedBullets.Add(bullet);
            }
            else
            {
                float clampedSpread = Mathf.Clamp(_stats.spread.GetValue(), 0f, 360f);
                float step = clampedSpread / bullets;
                float startAngle = -clampedSpread / 2f;

                for (int i = 0; i < bullets; i++)
                {
                    float angle = startAngle + step * i;
                    Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
                    var bullet = InitializeBullet(bulletRotation);
                    spawnedBullets.Add(bullet);
                }
            }

            if (spawnedBullets.Count <= 1) return;
            foreach (var bullet in spawnedBullets)
            {
                bullet.SetVolleySiblings(spawnedBullets);
            }
        }

        private BulletComponent InitializeBullet(Quaternion bulletRotation)
        {
            Vector3 bulletDirection = bulletRotation * Vector3.up;
            Vector3 bulletPosition = transform.position + bulletDirection * spawnDistance;
            
            var bullet = Instantiate(bulletPrefab, bulletPosition, bulletRotation);
            var bulletComponent = bullet.GetComponent<BulletComponent>();
            bulletComponent.Initialize(_stats, GetComponentInParent<Collider2D>());
            return bulletComponent;
        }

        private Quaternion GetBulletRotation()
        {
            if (_stats.spread.GetValue() <= 0) return transform.rotation;
            
            float clampedSpread = Mathf.Clamp(_stats.spread.GetValue(), 0f, 360f);
            float halfSpread = clampedSpread * 0.5f;
            float randomOffset = UnityEngine.Random.Range(-halfSpread, halfSpread);
            
            return transform.rotation * Quaternion.Euler(0f, 0f, randomOffset);
        }
    }
}