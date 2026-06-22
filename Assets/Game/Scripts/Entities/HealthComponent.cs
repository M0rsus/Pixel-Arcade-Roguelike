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

        [SerializeField]
        private UI.SliderView healthView;
        private StatInt _maxHealth;
        private StatFloat _healthRegen;
        [SerializeField]
        private StatInt _currentHealth;

        [SerializeField]
        private float delayRegen = 0.1f;
        
        public void Initialize(Stats stats, CancellationToken ct)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth.Value = _maxHealth.GetValue();
            _healthRegen = stats.healthRegen;
            if (healthView)
                healthView.Initialize(_currentHealth, _maxHealth);
            _ct = ct;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth.Value -= (int)damage;
            OnHealthNotFull?.Invoke();
            
            ClearCancellationTokenSource();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
            Regen().Forget();
            
            if (_currentHealth.Value <= 0)
                Die();
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, _maxHealth.GetValue());
        }

        public void Heal(float amount)
        {
            int maxHealth = _maxHealth.GetValue();
            _currentHealth.Value += (int)amount;
            
            if (_currentHealth.Value >= maxHealth) OnHealthFull?.Invoke();
            
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, maxHealth);
            
        }
        public async UniTaskVoid Regen()
        {
            try
            {
                while (_currentHealth.Value < _maxHealth.GetValue())
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
        public void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private void Die()
        {
            Debug.Log("Death");
            OnDeath?.Invoke();
        }
    }
}