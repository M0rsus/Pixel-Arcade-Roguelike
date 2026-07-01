using Game;
using UnityEngine;

namespace Cards
{
    public class GymEffect : Effect
    {
        private readonly Modifier<float> _health = new(0, Modifier<float>.ModifierType.Add);
        private readonly Modifier<int> _damage = new(0, Modifier<int>.ModifierType.Add);
        private readonly Modifier<float> _moveSpeed = new(0f, Modifier<float>.ModifierType.Add);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            _stats.maxHealth.AddModifier(_health);
            _stats.bulletDamage.AddModifier(_damage);
            _stats.moveSpeed.AddModifier(_moveSpeed);
            GameManager.OnEndWave += WaveCompleted;
        }

        private void WaveCompleted(float postWaveDelay)
        {
            _health.Value += 5f;
            _damage.Value += 5;
            _moveSpeed.Value += 0.1f;
        }

        public override void Destroy()
        {
            GameManager.OnEndWave -= WaveCompleted;
            _stats.maxHealth.RemoveModifier(_health);
            _stats.bulletDamage.RemoveModifier(_damage);
            _stats.moveSpeed.RemoveModifier(_moveSpeed);
        }
    }
}