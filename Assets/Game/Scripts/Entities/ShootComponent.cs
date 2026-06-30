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
        
        private readonly AsyncTimer _asyncTimer = new AsyncTimer();
        private readonly StatFloat _timer = new StatFloat(float.MaxValue);
        private bool _canShoot = true;
        private bool _isHoldingFire;
        
        public void Initialize(IShootable shootable, IBulletSpawner bulletSpawner, Stats stats)
        {
            _shootable = shootable;
            _shootable.OnShoot += TryShoot;
            _bulletSpawner = bulletSpawner;
            _cooldown = stats.shootCooldown;
            
            if (!cooldownView) return;
            cooldownView.Initialize(_timer, _cooldown);
            cooldownView.gameObject.SetActive(false);
            
            _bulletSpawner.Initialize(stats);
        }

        public void OnDestroy()
        {
            _shootable.OnShoot -= TryShoot;
        }

        public void OnUpdate()
        {
            if (_canShoot && _isHoldingFire)
                Shoot();
        }
        private void TryShoot(bool isPressed)
        {
            _isHoldingFire = isPressed;
        }

        public void Shoot()
        {
            _timer.Value = 0;
            _asyncTimer.Start(_cooldown.GetValue(), CurrentCooldown);
            _canShoot = false;
            
            _bulletSpawner.SpawnBullet();
            cooldownView.gameObject.SetActive(true);
            cooldownView.gameObject.transform.SetAsFirstSibling();
        }
        
        private void CurrentCooldown(StatFloat timer)
        {
            _timer.Value = timer.Value;
            if (_timer.Value >= _cooldown.GetValue()) EnableShooting();
        }

        private void EnableShooting()
        {
            _canShoot = true;
            cooldownView.gameObject.SetActive(false);
        }
    }
}