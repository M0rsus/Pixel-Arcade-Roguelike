using System;
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

        public void Initialize(MonoBehaviour entity, Stats stats)
        {
            _entity = entity;
            healthComponent.Initialize(stats);
            armorComponent.Initialize(stats);
            _regenArmorAtFullHealth = stats.regenArmorAtFullHealth;
            
            healthComponent.OnDeath += Death;
            _regenArmorAtFullHealth.OnUpdated += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        public void OnDestroy()
        {
            armorComponent.OnDestroy();
            healthComponent.OnDeath -= Death;
            healthComponent.OnHealthFull -= armorComponent.ActivateRegen;
            _regenArmorAtFullHealth.OnUpdated -= UpdateSubscriptions;
        }

        private void UpdateSubscriptions()
        {
            if (healthComponent == null || armorComponent == null) return;
            
            healthComponent.OnHealthFull -= armorComponent.ActivateRegen;
            if (_regenArmorAtFullHealth.GetValue())
                healthComponent.OnHealthFull += armorComponent.ActivateRegen;
        }

        public void TakeDamage(int damage)
        {
            float excessDamage = 0;
            
            if (armorComponent != null)
                excessDamage = armorComponent?.TakeDamage(damage) ?? 0;

            if (excessDamage <= 0)
            {
                armorComponent?.ActivateRegen();
                return;
            }
            if (!_regenArmorAtFullHealth.GetValue()) 
                armorComponent?.ActivateRegen();
            else 
                armorComponent?.CancelRegen();
            healthComponent?.TakeDamage(excessDamage);
        }

        private void Death()
        {
            Object.Destroy(_entity.gameObject);
            Statistics.EnemyKill();
        }
    }
}