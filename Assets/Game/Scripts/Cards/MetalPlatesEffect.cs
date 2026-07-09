using Game;
using UnityEngine;

namespace Cards
{
    public class MetalPlatesEffect : Effect
    {
        private readonly Modifier<float> _maxArmor = new(20f, Modifier<float>.ModifierType.Add);
        private readonly Modifier<float> _armorRegen = new(0.5f, Modifier<float>.ModifierType.Add);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.maxArmor.AddModifier(_maxArmor);
            stats.armorRegen.AddModifier(_armorRegen);
        }

        public override void Destroy()
        {
            _stats.maxArmor.RemoveModifier(_maxArmor);
            _stats.armorRegen.RemoveModifier(_armorRegen);
        }
    }
}