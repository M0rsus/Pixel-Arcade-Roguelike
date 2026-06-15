using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnManager enemySpawnManager;
        [SerializeField] 
        private List<WaveData> waves;
        [SerializeField] 
        private TextView currentWaveView;
        [SerializeField] 
        private SliderView progressWaveView;

        private StatInt _currentWave = new StatInt();

        private void Start()
        {
            if (progressWaveView)
                progressWaveView.Initialize(
                    LevelContext.Instance.currentEnemyCount,
                    LevelContext.Instance.maxEnemyCount);
            if (currentWaveView)
                currentWaveView.Initialize(_currentWave);
        }
        private void StartWave()
        {
            WaveData wave = waves[_currentWave.Value];
            enemySpawnManager.InitSpawn(wave);
            _currentWave.Value++;
        }
        private void OnEnable()
        {
            GameManager.OnStartWave += StartWave;
        }

        private void OnDisable()
        {
            GameManager.OnStartWave -= StartWave;
        }
    }
}