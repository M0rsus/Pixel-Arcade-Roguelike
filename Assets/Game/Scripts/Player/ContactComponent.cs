using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ContactComponent
    {
        private IEntity _entityType;
        private StatInt _contactDamage;
        private StatFloat _contactDamageCooldown;

        private float _timer;

        public void Initialize(Stats stats, IEntity type)
        {
            _entityType = type;
            _contactDamage = stats.contactDamage;
            _contactDamageCooldown = stats.contactDamageCooldown;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            _timer += deltaTime;
        }
        
        public void OnContact(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IEntity>(out var component)
                || _timer < _contactDamageCooldown.GetValue()) return;
            if (_entityType is IEnemy && component is IEnemy) return;
            component.Damageable.TakeDamage(_contactDamage.GetValue());
            _timer = 0;
        }

        private enum EntityType
        {
            Player,
            Enemy
        }
    }
}