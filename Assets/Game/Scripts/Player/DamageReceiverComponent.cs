using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class DamageReceiverComponent : IDamageable
    {
        [SerializeField]
        private HealthComponent healthComponent;
        [SerializeField]
        private ArmorComponent armorComponent;
        
        private StatBool _regenArmorAtFullHealth;
        private bool _prevRegenArmorAtFullHealth;

        private float _excessDamage;

        public void Initialize(Stats stats)
        {
            healthComponent.Initialize(stats);
            armorComponent.Initialize(stats);
            _regenArmorAtFullHealth = stats.regenArmorAtFullHealth;
            UpdateSubscriptions();
        }

        private void ActivateArmorRegen()
        {
            Debug.Log("<color=green>ActivateArmorRegen</color>");
            armorComponent.IsRegenAllowed = true;
        }

        private void DeactivateArmorRegen()
        {
            Debug.Log("<color=red>DeactivateArmorRegen</color>");
            armorComponent.IsRegenAllowed = false;
        }

        public void OnDisable()
        {
            healthComponent.OnHealthFull -= ActivateArmorRegen;
            healthComponent.OnHealthNotFull -= DeactivateArmorRegen;
        }

        public void UpdateSubscriptions()
        {
            if (healthComponent == null || armorComponent == null) return;
            
            healthComponent.OnHealthFull -= ActivateArmorRegen;
            healthComponent.OnHealthNotFull -= DeactivateArmorRegen;

            if (!_regenArmorAtFullHealth.GetValue())
            {
                ActivateArmorRegen();
                return;
            }

            healthComponent.OnHealthFull += ActivateArmorRegen;
            healthComponent.OnHealthNotFull += DeactivateArmorRegen;
        }

        public void TakeDamage(float damage)
        {
            if (armorComponent != null)
                _excessDamage = armorComponent.TakeDamage(damage);
            if (_excessDamage <= 0)
                return;
            healthComponent?.TakeDamage(damage);
        }
        public void OnUpdate(float deltaTime)
        {
            if (_regenArmorAtFullHealth.GetValue() != _prevRegenArmorAtFullHealth)
            {
                UpdateSubscriptions();
                _prevRegenArmorAtFullHealth = _regenArmorAtFullHealth.GetValue();
            }
            armorComponent?.OnUpdate(deltaTime);
            healthComponent?.OnUpdate(deltaTime);
        }
    }
}