using System;
using System.ComponentModel;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ArmorComponent
    {
        public static event Action OnArmorLost;
        
        private StatInt _maxArmor;
        private StatFloat _armorRegen;
        [SerializeField]
        private float _currentArmor;

        [SerializeField] 
        private float cooldownArmorRegen = 4f;
        [SerializeField]
        private float delayRegen = 1f;

        private float _timer;
        private bool _canArmorRegen;

        private bool _isRegenAllowed;
        public bool IsRegenAllowed
        {
            get => _isRegenAllowed;
            set
            {
                _isRegenAllowed = value;
                if (!value)
                    _timer = 0;
            }
        }

        public void Initialize(Stats stats)
        {
            _maxArmor = stats.maxArmor;
            _currentArmor = _maxArmor.GetValue();
            _armorRegen = stats.armorRegen;
        }

        public float TakeDamage(float damage)
        {
            float prevArmor = _currentArmor;
            _currentArmor -= damage;
            float excessDamage = -_currentArmor;
            if (_currentArmor <= 0)
            {
                if (prevArmor > 0)
                    OnArmorLost?.Invoke();
                _currentArmor = 0;
            }
            _timer = 0;
            _canArmorRegen = false;
            return excessDamage;
        }
        
        public void Heal(float amount)
        {
            _currentArmor += amount;
            if (_currentArmor > _maxArmor.GetValue())
                _currentArmor = _maxArmor.GetValue();
        }

        public void Regen()
        {
            float regenArmor = _armorRegen.GetValue() * delayRegen;
            Heal(regenArmor);
            _timer = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_armorRegen.GetValue() == 0)
                return;
            
            if (IsRegenAllowed)
                _timer += deltaTime;
            
            if (_timer >= cooldownArmorRegen && !_canArmorRegen)
                _canArmorRegen = true;
            
            if (!_canArmorRegen)
                return;
            
            if (_timer > delayRegen && _currentArmor < _maxArmor.GetValue())
                Regen();
        }
    }
}