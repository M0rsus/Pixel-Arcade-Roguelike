using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game;
using UnityEngine;

namespace Cards
{
    public class PassAmuletEffect : Effect
    {
        private static int _activeCardsCount;
        private static int _activeEffectsCount;
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private static readonly Modifier<float> _shootCooldown = new(0.5f, Modifier<float>.ModifierType.Multiply);
        private static readonly Modifier<float> _bulletSpeed = new(2f, Modifier<float>.ModifierType.Multiply);
        private static readonly StatFloat _effectCooldown = new(5f);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            _activeCardsCount++;
            Barriers.OnDoorCrossed += BarrierCrossed;
        }

        private void BarrierCrossed()
        {
            if (_activeEffectsCount == 0)
            {
                _shootCooldown.Value = 1f / (1 << _activeCardsCount);
                _bulletSpeed.Value = 1f + _activeCardsCount;
                _effectCooldown.Value = 5f + 1f * _activeCardsCount;
            
                _stats.shootCooldown.AddModifier(_shootCooldown);
                _stats.bulletSpeed.AddModifier(_bulletSpeed);
            }
            _activeEffectsCount++;
            DurationEffect().Forget();
        }

        private async UniTaskVoid DurationEffect()
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_effectCooldown.Value),
                    ignoreTimeScale: false,
                    cancellationToken: _ct);
            }
            catch (OperationCanceledException) { }
            finally
            {
                _activeEffectsCount--;
                if (_activeEffectsCount == 0)
                {
                    _stats.shootCooldown.RemoveModifier(_shootCooldown);
                    _stats.bulletSpeed.RemoveModifier(_bulletSpeed);
                }
            }
        }

        public override void Destroy()
        {
            _activeCardsCount--;
            Barriers.OnDoorCrossed -= BarrierCrossed;
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}