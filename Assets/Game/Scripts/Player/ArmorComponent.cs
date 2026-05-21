using System;
using System.ComponentModel;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ArmorComponent
    {
        public event Action OnArmorLost;

        public StatInt MaxArmor { get; private set; }
        
        private StatFloat _armorRegen;
        [SerializeField]
        private float _currentArmor;

        [SerializeField] 
        private float cooldownArmorRegen = 4f;
        [SerializeField]
        private float delayRegen = 1f;

        private float _timer;
        private bool _canArmorRegen;
        public bool HasRegen { get; private set; }

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
            MaxArmor = stats.maxArmor;
            _currentArmor = MaxArmor.GetValue();
            _armorRegen = stats.armorRegen;
            MaxArmor.OnValueChange += CheckArmor;
        }

        public void OnDisable()
        {
            MaxArmor.OnValueChange -= CheckArmor;
        }

        private void CheckArmor()
        {
            HasRegen = MaxArmor.GetValue() > 0;
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
            if (_currentArmor > MaxArmor.GetValue())
                _currentArmor = MaxArmor.GetValue();
        }

        private void Regen(float deltaTime)
        {
            if (IsRegenAllowed)
                _timer += deltaTime;
            
            if (_timer >= cooldownArmorRegen && !_canArmorRegen)
                _canArmorRegen = true;
            
            if (!_canArmorRegen) return;

            if (!(_timer > delayRegen) || !(_currentArmor < MaxArmor.GetValue())) return;
            
            float regenArmor = _armorRegen.GetValue() * delayRegen;
            Heal(regenArmor);
            _timer = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (HasRegen)
                Regen(deltaTime);
        }
    }
}