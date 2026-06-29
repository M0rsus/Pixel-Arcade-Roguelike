using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class ShootComponent
    {
        [SerializeField]
        private UI.SliderView cooldownView;
        private IShootable _shootable;
        private IBulletSpawner _bulletSpawner;
        private StatFloat _cooldown;
        
        private StatFloat _timer = new (float.MaxValue);
        
        public void Initialize(IShootable shootable, IBulletSpawner bulletSpawner, Stats stats)
        {
            _shootable = shootable;
            _bulletSpawner = bulletSpawner;
            _cooldown = stats.shootCooldown;
            
            if (cooldownView)
                cooldownView.Initialize(_timer, _cooldown);
            _bulletSpawner.Initialize(stats);
        }

        public void OnUpdate(float deltaTime)
        {
            _timer.Value += deltaTime;
            cooldownView.gameObject.SetActive(_timer.Value < _cooldown.GetValue());
            TryShoot();
        }

        public void TryShoot()
        {
            if (_shootable.ShootInput && _timer.Value > _cooldown.GetValue())
                Shoot();
        }

        public void Shoot()
        {
            _timer.Value = 0;
            _bulletSpawner.SpawnBullet();
            cooldownView.gameObject.transform.SetAsFirstSibling();
        }
    }
}