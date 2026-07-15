using Game;
using UnityEngine;

namespace Cards
{
    public class SupersonicPowderEffect : Effect
    {
        private readonly Modifier<float> _range = new(1.2f, Modifier<float>.ModifierType.Multiply);
        private readonly Modifier<float> _bulletSpeed = new(1.4f, Modifier<float>.ModifierType.Multiply);
        private readonly Modifier<float> _lifeTimeBullet = new(0.8f, Modifier<float>.ModifierType.Multiply);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.range.AddModifier(_range);
            stats.bulletSpeed.AddModifier(_bulletSpeed);
            stats.lifeTime.AddModifier(_lifeTimeBullet);
        }

        public override void Destroy()
        {
            _stats.range.RemoveModifier(_range);
            _stats.bulletSpeed.RemoveModifier(_bulletSpeed);
            _stats.lifeTime.RemoveModifier(_lifeTimeBullet);
        }
    }
}