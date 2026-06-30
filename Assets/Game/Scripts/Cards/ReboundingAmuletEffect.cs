using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game;
using UnityEngine;

namespace Cards
{
    public class ReboundingAmuletEffect : Effect
    {
        private static int _activeCardsCount;
        private static int _activeEffectsCount;
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private static readonly Modifier<float> _healthRegen = new(5f, Modifier<float>.ModifierType.Add);
        private static readonly StatFloat _effectCooldown = new(4f);
        public override void Initialize(Stats stats, GameObject entity)
        {
            base.Initialize(stats, entity);
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            _activeCardsCount++;
            Barriers.OnBarrierCrossed += BarrierCrossed;
        }

        private void BarrierCrossed()
        {
            if (_activeEffectsCount == 0)
            {
                _healthRegen.Value = 5f * _activeCardsCount;
                _effectCooldown.Value = 3f + 1f * _activeCardsCount;
            
                _stats.healthRegen.AddModifier(_healthRegen);
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
                    _stats.healthRegen.RemoveModifier(_healthRegen);
                }
            }
        }

        public override void Destroy()
        {
            _activeCardsCount--;
            Barriers.OnBarrierCrossed -= BarrierCrossed;
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}