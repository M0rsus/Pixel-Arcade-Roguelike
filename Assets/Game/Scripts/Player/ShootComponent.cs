using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ShootComponent
    {
        private IShootable _shootable;
        private IBulletSpawner _bulletSpawner;
        private StatFloat _cooldown;
        
        private float _timer = float.MaxValue;
        
        public void Initialize(IShootable shootable, IBulletSpawner bulletSpawner, Stats stats)
        {
            _shootable = shootable;
            _bulletSpawner = bulletSpawner;
            _cooldown = stats.shootCooldown;
            
            _bulletSpawner.Initialize(stats);
        }

        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;
            TryShoot();
        }

        public void TryShoot()
        {
            if (_shootable.ShootInput && _timer > _cooldown.GetValue())
                Shoot();
        }

        public void Shoot()
        {
            _timer = 0;
            _bulletSpawner.SpawnBullet();
        }
    }
}