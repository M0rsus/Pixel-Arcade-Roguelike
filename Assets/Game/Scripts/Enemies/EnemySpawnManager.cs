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
        
        private int _points;
        private List<EnemySpawnOption> _enemySpawnOptions;
        private List<GameObject> _listEnemies;

        private void Awake()
        {
            _ct = CancellationToken.None;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
        }

        public void InitSpawn(WaveData wave)
        {
            _points = wave.points;
            _enemySpawnOptions = wave.enemySpawnOptions;
            _listEnemies = new List<GameObject>();
            
            ClearCancellationTokenSource();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
            SpawnListEnemies();
        }

        private void SpawnListEnemies()
        {
            try
            {
                float totalWeight = 0;
                int minPoints = int.MaxValue;
                foreach (var enemySpawnOption in _enemySpawnOptions)
                {
                    totalWeight += enemySpawnOption.weight;
                    if (enemySpawnOption.points < minPoints)
                        minPoints = enemySpawnOption.points;
                }
                
                while (_points > 0)
                {
                    float randomValue = Random.Range(0, totalWeight);
                    float currentWeight = 0;

                    foreach (var enemySpawnOption in _enemySpawnOptions)
                    {
                        currentWeight += enemySpawnOption.weight;
                        if (randomValue > currentWeight) continue;
                    
                        if (enemySpawnOption.points > _points)
                        {
                            if (_points < minPoints)
                            {
                                _points = 0;
                                break;
                            }
                            continue;
                        }
                        Debug.Log(enemySpawnOption.prefab.name);
                        _listEnemies.Add(enemySpawnOption.prefab);
                        _points -= enemySpawnOption.points;
                        LevelContext.Instance.currentEnemyCount.Value++;
                        LevelContext.Instance.maxEnemyCount.Value++;
                        break;
                    }
                }
                SpawnEnemies().Forget();
            }
            catch (OperationCanceledException) { }
        }

        private async UniTaskVoid SpawnEnemies()
        {
            foreach (var enemy in _listEnemies)
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(0.5f), 
                    ignoreTimeScale: false, 
                    cancellationToken: _cts.Token);
                enemySpawn.Instantiate(enemy);
            }
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