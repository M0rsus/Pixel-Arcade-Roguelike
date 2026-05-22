using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class HealthComponent
    {
        public event Action OnHealthFull;
        public event Action OnHealthNotFull;
        public event Action OnDeath;
        
        private CancellationToken _ct;
        private CancellationTokenSource _cts;
        
        private StatInt _maxHealth;
        private StatFloat _healthRegen;
        [SerializeField]
        private float _currentHealth;

        [SerializeField]
        private float delayRegen = 0.1f;
        
        public void Initialize(Stats stats, CancellationToken ct)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth = _maxHealth.GetValue();
            _healthRegen = stats.healthRegen;
            _ct = ct;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            OnHealthNotFull?.Invoke();
            CancelRegen();
            Regen().Forget();
            if (_currentHealth <= 0)
                Die();
        }

        public void Heal(float amount)
        {
            int maxHealth = _maxHealth.GetValue();
            _currentHealth += amount;
            
            if (_currentHealth >= maxHealth) OnHealthFull?.Invoke();
            
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            
        }
        public async UniTaskVoid Regen()
        {
            try
            {
                while (_currentHealth < _maxHealth.GetValue())
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delayRegen),
                        ignoreTimeScale: false,
                        cancellationToken: _cts.Token);
                
                    float regenArmor = _healthRegen.GetValue() * delayRegen;
                    Heal(regenArmor);
                }
            }
            catch (OperationCanceledException) { }
        }
        public void CancelRegen()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
        }

        private void Die()
        {
            Debug.Log("Death");
            OnDeath?.Invoke();
        }
    }
}