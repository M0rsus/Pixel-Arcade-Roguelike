using Game;
using UnityEngine;

namespace Cards
{
    public class OverclockedFanEffect : Effect
    {
        private readonly Modifier<float> _moveSpeed = new(1.05f, Modifier<float>.ModifierType.Multiply);
        private readonly Modifier<float> _rotationSpeed = new(20f, Modifier<float>.ModifierType.Add);
        private readonly Modifier<float> _maxHealth = new(5f, Modifier<float>.ModifierType.Subtract);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.moveSpeed.AddModifier(_moveSpeed);
            stats.rotationSpeed.AddModifier(_rotationSpeed);
            stats.maxHealth.AddModifier(_maxHealth);
        }

        public override void Destroy()
        {
            _stats.moveSpeed.RemoveModifier(_moveSpeed);
            _stats.rotationSpeed.RemoveModifier(_rotationSpeed);
            _stats.maxHealth.RemoveModifier(_maxHealth);
        }
    }
}