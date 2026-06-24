using System;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

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

        private MonoBehaviour _entity;
        private float _excessDamage;

        public void Initialize(MonoBehaviour entity, Stats stats, CancellationToken ct)
        {
            _entity = entity;
            healthComponent.Initialize(stats, ct);
            armorComponent.Initialize(stats, ct);
            _regenArmorAtFullHealth = stats.regenArmorAtFullHealth;
            
            armorComponent.MaxArmor.OnUpdated += UpdateSubscriptions;
            _regenArmorAtFullHealth.OnUpdated += UpdateSubscriptions;
            healthComponent.OnDeath += Death;
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
            if (healthComponent != null) healthComponent.OnHealthFull -= ActivateArmorRegen;
            if (armorComponent != null) armorComponent.MaxArmor.OnUpdated -= UpdateSubscriptions;
            if (_regenArmorAtFullHealth != null) _regenArmorAtFullHealth.OnUpdated -= UpdateSubscriptions;
        }

        public void OnDestroy()
        {
            healthComponent.ClearCancellationTokenSource();
            armorComponent.ClearCancellationTokenSource();
            healthComponent.OnDeath -= Death;
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

        private void Death()
        {
            Object.Destroy(_entity.gameObject);
        }
    }
}