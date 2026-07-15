using Game;
using UnityEngine;

namespace Cards
{
    public class BouncyBulletEffect : Effect
    {
        private readonly Modifier<int> _bounces = new(1, Modifier<int>.ModifierType.Add);
        private readonly Modifier<int> _damageBullet = new(5, Modifier<int>.ModifierType.Subtract);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            stats.bounces.AddModifier(_bounces);
            stats.bulletDamage.AddModifier(_damageBullet);
        }

        public override void Destroy()
        {
            _stats.bounces.RemoveModifier(_bounces);
            _stats.bulletDamage.RemoveModifier(_damageBullet);
        }
    }
}