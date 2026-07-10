using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class AsyncTimer
    {
        private CancellationTokenSource _cts;
        private CancellationToken _ct;

        public void Start(StatFloat duration, Action<StatFloat> callback)
        {
            Stop();
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            Run(duration, callback).Forget();
        }

        private async UniTaskVoid Run(StatFloat duration, Action<StatFloat> callback)
        {
            StatFloat timer = new();

            try
            {
                while (timer.Value < duration.GetValue())
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, _ct);

                    timer.Value += Time.deltaTime;

                    callback?.Invoke(timer);
                }
            }
            catch (OperationCanceledException) { }
        }

        public void Stop()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}