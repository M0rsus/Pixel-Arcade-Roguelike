using Game;
using UnityEngine;

namespace Cards
{
    public class LongBulletEffect : Effect
    {
        private readonly Modifier<float> _range = new(4f, Modifier<float>.ModifierType.Add);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.range.AddModifier(_range);
        }

        public override void Destroy()
        {
            _stats.range.RemoveModifier(_range);
        }
    }
}