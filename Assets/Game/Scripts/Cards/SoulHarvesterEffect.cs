using Game;
using UnityEngine;

namespace Cards
{
    public class SoulHarvesterEffect : Effect
    {
        private int _enemiesKilledCount;
        private readonly Modifier<int> _damage = new(0, Modifier<int>.ModifierType.Add);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            _stats.bulletDamage.AddModifier(_damage);
            Statistics.OnEnemyKill += EnemyDeath;
        }

        private void EnemyDeath()
        {
            _enemiesKilledCount++;
            if (_enemiesKilledCount < 5) return;
            _damage.Value += 1;
            _enemiesKilledCount = 0;
        }

        public override void Destroy()
        {
            Statistics.OnEnemyKill -= EnemyDeath;
            _stats.bulletDamage.RemoveModifier(_damage);
        }
    }
}