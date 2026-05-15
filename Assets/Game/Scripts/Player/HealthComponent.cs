using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class HealthComponent : IDamageable
    {
        private Stat _maxHealth;
        private float _currentHealth;

        public static event Action OnDeath;
        
        public void Initialize(Stats stats)
        {
            _maxHealth = stats.maxHealth;
            _currentHealth = _maxHealth.GetValue();
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("Death");
            OnDeath?.Invoke();
        }
    }
}