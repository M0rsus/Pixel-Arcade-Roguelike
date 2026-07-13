using Game;
using UnityEngine;

namespace Cards
{
    public class AmmunitionEffect : Effect
    {
        private readonly Modifier<float> _shootCooldown = new(0.8f, Modifier<float>.ModifierType.Multiply);
        private readonly Modifier<float> _rotationSpeed = new(0.95f, Modifier<float>.ModifierType.Multiply);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.shootCooldown.AddModifier(_shootCooldown);
            stats.rotationSpeed.AddModifier(_rotationSpeed);
        }

        public override void Destroy()
        {
            _stats.shootCooldown.RemoveModifier(_shootCooldown);
            _stats.moveSpeed.RemoveModifier(_rotationSpeed);
        }
    }
}