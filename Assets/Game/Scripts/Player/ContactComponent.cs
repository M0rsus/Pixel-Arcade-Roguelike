using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ContactComponent
    {
        private StatInt _contactDamage;
        private StatFloat _contactDamageCooldown;

        private float _timer;

        public void Initialize(Stats stats)
        {
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
                || !(_timer >= _contactDamageCooldown.GetValue())) return;
            component.Damageable.TakeDamage(_contactDamage.GetValue());
            _timer = 0;
        }
    }
}