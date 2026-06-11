using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnManager enemySpawnManager;
        [SerializeField] 
        private List<WaveData> waves;
        
        private int currentWave = 0;
        public int CurrentWave => currentWave;
        public int Waves => waves.Count;

        private void StartWave()
        {
            WaveData wave = waves[currentWave];
            enemySpawnManager.InitSpawn(wave);
            currentWave++;
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