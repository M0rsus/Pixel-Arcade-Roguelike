using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class EnemySpawnManager : MonoBehaviour
    {
        private CancellationToken _ct;
        private CancellationTokenSource _cts;
        
        [SerializeField] 
        private EnemySpawn enemySpawn;
        
        private int points;
        private List<EnemySpawnOption> enemySpawnOptions;

        private void Awake()
        {
            _ct = CancellationToken.None;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
        }

        public void InitSpawn(WaveData wave)
        {
            points = wave.points;
            enemySpawnOptions = wave.enemySpawnOptions;
            
            ClearCancellationTokenSource();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
            SpawnEnemies().Forget();
        }

        private async UniTaskVoid SpawnEnemies()
        {
            try
            {
                float totalWeight = 0;
                int minPoints = int.MaxValue;
                foreach (var enemySpawnOption in enemySpawnOptions)
                {
                    totalWeight += enemySpawnOption.weight;
                    if (enemySpawnOption.points < minPoints)
                        minPoints = enemySpawnOption.points;
                }
                
                while (points > 0)
                {
                    float randomValue = Random.Range(0, totalWeight);
                    float currentWeight = 0;

                    foreach (var enemySpawnOption in enemySpawnOptions)
                    {
                        currentWeight += enemySpawnOption.weight;
                        if (randomValue > currentWeight) continue;
                    
                        if (enemySpawnOption.points > points)
                        {
                            if (points < minPoints)
                            {
                                points = 0;
                                break;
                            }
                            continue;
                        }
                        await UniTask.Delay(
                            TimeSpan.FromSeconds(0.5f), 
                            ignoreTimeScale: false, 
                            cancellationToken: _cts.Token);
                        Debug.Log(enemySpawnOption.prefab.name);
                        enemySpawn.Instantiate(enemySpawnOption.prefab);
                        points -= enemySpawnOption.points;
                        break;
                    }
                }
            }
            catch (OperationCanceledException) { }
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