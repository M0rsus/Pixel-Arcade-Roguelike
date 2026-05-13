using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ShootComponent
    {
        [SerializeField]
        private float cooldown;
        
        private IControllable _controllable;
        private IBulletSpawner _bulletSpawner;
        private float _timer = float.MaxValue;
        
        public void Initialize(IControllable controllable, IBulletSpawner bulletSpawner)
        {
            _controllable = controllable;
            _bulletSpawner = bulletSpawner;
        }

        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;
            TryShoot();
        }

        public void TryShoot()
        {
            if (_controllable.ShootInput && _timer > cooldown)
                Shoot();
        }

        public void Shoot()
        {
            _timer = 0;
            _bulletSpawner.SpawnBullet();
        }
    }
}