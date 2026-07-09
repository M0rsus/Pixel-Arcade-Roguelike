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
        private StatFloat _maxHealth;
        private StatFloat _healthRegen;
        private StatFloat _currentHealth;
        private bool _isActiveRegen;

        [SerializeField]
        private float delayRegen = 0.1f;
        
        public void Initialize(Stats stats)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth = new (_maxHealth.GetValue());
            _healthRegen = stats.healthRegen;
            if (healthView)
                healthView.Initialize(_currentHealth, _maxHealth);
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            _maxHealth.OnChanged += Recalculate;
            _maxHealth.OnUpdated += ShouldRegenerate;
            _currentHealth.OnUpdated += ShouldRegenerate;
            _healthRegen.OnUpdated += ShouldRegenerate;
        }

        private void Recalculate(float oldHealth, float newHealth)
        {
            Heal(newHealth - oldHealth);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth.Value -= damage;
            OnHealthNotFull?.Invoke();
            
            if (_currentHealth.Value <= 0)
                Die();
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, _maxHealth.GetValue());
        }

        public void Heal(float amount)
        {
            float maxHealth = _maxHealth.GetValue();
            _currentHealth.Value += amount;
            
            if (_currentHealth.Value >= maxHealth) OnHealthFull?.Invoke();
            
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, maxHealth);
            
        }
        private async UniTaskVoid Regen()
        {
            try
            {
                while (_currentHealth.Value < _maxHealth.GetValue())
                {
                    _isActiveRegen = true;
                    await UniTask.Delay(TimeSpan.FromSeconds(delayRegen),
                        ignoreTimeScale: false,
                        cancellationToken: _cts.Token);
                    
                    float healAmount = _healthRegen.GetValue() * delayRegen;
                    Heal(healAmount);
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                _isActiveRegen = false;
            }
        }

        private void ShouldRegenerate()
        {
            if (_currentHealth.Value >= _maxHealth.GetValue() || _isActiveRegen) return;
            CancelRegen();
            Regen().Forget();
        }
        private void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
        
        public void CancelRegen()
        {
            ClearCancellationTokenSource();
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
        }

        private void Die()
        {
            ClearCancellationTokenSource();
            OnDeath?.Invoke();
            _maxHealth.OnChanged -= Recalculate;
            _maxHealth.OnUpdated -= ShouldRegenerate;
            _currentHealth.OnUpdated -= ShouldRegenerate;
            _healthRegen.OnUpdated -= ShouldRegenerate;
        }
    }
}