using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class HealthComponent
    {
        public event Action OnDeath;
        public event Action OnHealthFull;
        public event Action OnHealthNotFull;
        
        private StatInt _maxHealth;
        private StatFloat _healthRegen;
        [SerializeField]
        private float _currentHealth;

        [SerializeField]
        private float delayRegen = 0.1f;
        
        private float _timer;
        
        public void Initialize(Stats stats)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth = _maxHealth.GetValue();
            _healthRegen = stats.healthRegen;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            OnHealthNotFull?.Invoke();
            if (_currentHealth <= 0)
                Die();
        }

        public void Heal(float amount)
        {
            _currentHealth += amount;
            Debug.Log("Current Health: " + _currentHealth);
            if (_currentHealth >= _maxHealth.GetValue())
            {
                _currentHealth = _maxHealth.GetValue();
                OnHealthFull?.Invoke();
            }
        }

        public void Regen()
        {
            float regenHealth = _healthRegen.GetValue() * delayRegen;
            Heal(regenHealth);
            _timer = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_healthRegen.GetValue() == 0)
                return;
            
            _timer += deltaTime;
            if (_timer > delayRegen && _currentHealth < _maxHealth.GetValue())
                Regen();
        }

        private void Die()
        {
            Debug.Log("Death");
            OnDeath?.Invoke();
        }
    }
}