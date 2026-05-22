using System;
using System.Threading;
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

        private float _excessDamage;

        public void Initialize(Stats stats, CancellationToken ct)
        {
            healthComponent.Initialize(stats, ct);
            armorComponent.Initialize(stats, ct);
            _regenArmorAtFullHealth = stats.regenArmorAtFullHealth;
            
            armorComponent.MaxArmor.OnValueChange += UpdateSubscriptions;
            _regenArmorAtFullHealth.OnValueChange += UpdateSubscriptions;
            UpdateSubscriptions();
            if (armorComponent.CheckArmor())
                ActivateArmorRegen();
        }

        private void ActivateArmorRegen()
        {
            Debug.Log("<color=green>ActivateArmorRegen</color>");
            armorComponent.IsActiveRegen = true;
            armorComponent.Regen().Forget();
        }

        private void DeactivateArmorRegen()
        {
            Debug.Log("<color=red>DeactivateArmorRegen</color>");
            armorComponent.IsActiveRegen = false;
        }

        public void OnDisable()
        {
            healthComponent.OnHealthFull -= ActivateArmorRegen;
            armorComponent.MaxArmor.OnValueChange -= UpdateSubscriptions;
            _regenArmorAtFullHealth.OnValueChange -= UpdateSubscriptions;
            armorComponent.CancelRegen();
        }

        public void UpdateSubscriptions()
        {
            if (healthComponent == null || armorComponent == null) return;
            
            healthComponent.OnHealthFull -= ActivateArmorRegen;
            armorComponent.OnArmorLost -= DeactivateArmorRegen;

            if (!_regenArmorAtFullHealth.GetValue())
            {
                if (armorComponent.CheckArmor())
                    ActivateArmorRegen();
                return;
            }
            
            healthComponent.OnHealthFull += ActivateArmorRegen;
            armorComponent.OnArmorLost += DeactivateArmorRegen;
        }

        public void TakeDamage(float damage)
        {
            if (armorComponent != null)
                _excessDamage = armorComponent.TakeDamage(damage);
            
            if (_excessDamage <= 0) return;
            healthComponent?.TakeDamage(damage);
        }
    }
}