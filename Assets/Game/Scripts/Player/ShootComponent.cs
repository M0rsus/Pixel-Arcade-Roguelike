using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ShootComponent
    {
        private Stat _cooldown;
        
        private IControllable _controllable;
        private IBulletSpawner _bulletSpawner;
        private float _timer = float.MaxValue;
        
        public void Initialize(Stats stats, IControllable controllable, IBulletSpawner bulletSpawner)
        {
            _cooldown = stats.cooldown;
            _controllable = controllable;
            _bulletSpawner = bulletSpawner;
            _bulletSpawner.Initialize(stats);
        }

        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;
            TryShoot();
        }

        public void TryShoot()
        {
            if (_controllable.ShootInput && _timer > _cooldown.GetValue())
                Shoot();
        }

        public void Shoot()
        {
            _timer = 0;
            _bulletSpawner.SpawnBullet();
        }
    }
}