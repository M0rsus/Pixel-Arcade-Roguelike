using Game;
using UnityEngine;

namespace Cards
{
    public class EngineEffect : Effect
    {
        private readonly Modifier<float> _moveSpeed = new(3f, Modifier<float>.ModifierType.Add);
        private readonly Modifier<float> _shootCooldown = new(0.5f, Modifier<float>.ModifierType.Add);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.moveSpeed.AddModifier(_moveSpeed);
            stats.shootCooldown.AddModifier(_shootCooldown);
        }

        public override void Destroy()
        {
            _stats.moveSpeed.RemoveModifier(_moveSpeed);
            _stats.shootCooldown.RemoveModifier(_shootCooldown);
        }
    }
}