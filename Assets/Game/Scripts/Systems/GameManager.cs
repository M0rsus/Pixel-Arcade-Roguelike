using System;
using System.Threading;
using Cards;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnStartWave;
        public static event Action<float> OnEndWave;
        public static event Action OnStartCardSelection;
        public static event Action OnEndCardSelection;
        
        [SerializeField]
        private float postWaveDelay;
        
        private CancellationToken _ct;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _ct = CancellationToken.None;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
        }

        private void Start()
        {
            StartWave();
        }
        private void StartWave()
        {
            OnStartWave?.Invoke();
        }

        private async UniTaskVoid EndWave()
        {
            try
            {
                Debug.Log("End wave(BEGIN)");
                OnEndWave?.Invoke(postWaveDelay);
                await UniTask.Delay(
                    TimeSpan.FromSeconds(postWaveDelay),
                    ignoreTimeScale: false,
                    cancellationToken: _cts.Token);
                Debug.Log("End wave(END)");
                StartCardSelection();
            } 
            catch(OperationCanceledException) { }
        }
        
        private void StartCardSelection()
        {
            OnStartCardSelection?.Invoke();
        }
        
        private void EndCardSelection()
        {
            OnEndCardSelection?.Invoke();
            StartWave();
        }

        private void CheckProgressWave(int oldCount, int newCount)
        {
            if (newCount <= 0)
                EndWave().Forget();
        }

        private void OnEnable()
        {
            LevelContext.Instance.currentEnemyCount.OnChanged += CheckProgressWave;
            Card.OnPicked += EndCardSelection;
        }

        private void OnDisable()
        {
            LevelContext.Instance.currentEnemyCount.OnChanged -= CheckProgressWave;
            Card.OnPicked -= EndCardSelection;
        }
        private void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private void OnDestroy()
        {
            ClearCancellationTokenSource();
        }
    }
}