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
        private bool _activeRegen;

        [SerializeField]
        private float delayRegen = 0.1f;
        
        public void Initialize(Stats stats, CancellationToken ct)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth = new (_maxHealth.GetValue());
            _healthRegen = stats.healthRegen;
            if (healthView)
                healthView.Initialize(_currentHealth, _maxHealth);
            _ct = ct;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            _maxHealth.OnChanged += RecalculateHealth;
            _maxHealth.OnUpdated += ShouldRegenerate;
            _currentHealth.OnUpdated += ShouldRegenerate;
            _healthRegen.OnUpdated += ShouldRegenerate;
        }

        private void RecalculateHealth(float oldHealth, float newHealth)
        {
            Debug.Log("<color=green>Recalculate Health</color>");
            Debug.Log($"newHealth: {newHealth}");
            Debug.Log($"oldHealth: {oldHealth}");
            Debug.Log($"Recalculate Health: {_currentHealth.Value} & newHealth - oldHealth: {newHealth - oldHealth}");
            Heal(newHealth - oldHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Value -= damage;
            OnHealthNotFull?.Invoke();
            
            ClearCancellationTokenSource();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
            
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
                    _activeRegen = true;
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
                _activeRegen = false;
            }
        }

        private void ShouldRegenerate()
        {
            if (_currentHealth.Value < _maxHealth.GetValue() && _activeRegen == false)
                Regen().Forget();
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
            _maxHealth.OnChanged -= RecalculateHealth;
            _maxHealth.OnUpdated -= ShouldRegenerate;
            _currentHealth.OnUpdated -= ShouldRegenerate;
            _healthRegen.OnUpdated -= ShouldRegenerate;
        }
    }
}