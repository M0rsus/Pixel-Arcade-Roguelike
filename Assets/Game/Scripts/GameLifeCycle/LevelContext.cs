using System;
using UnityEngine;

namespace Game
{
    public class LevelContext : MonoBehaviour
    {
        public static LevelContext Instance { get; private set; }
        
        [field: SerializeField] 
        public Transform PlayerTransform { get; private set; }

        [NonSerialized]
        public StatInt currentEnemyCount = new StatInt();
        [NonSerialized]
        public StatInt maxEnemyCount = new StatInt();

        private void Awake()
        {
            Instance = this;
        }

        private void UpdateMaxCount()
        {
            maxEnemyCount.Value = 0;
        }
        private void OnEnable()
        {
            GameManager.OnStartWave += UpdateMaxCount;
        }

        private void OnDisable()
        {
            GameManager.OnStartWave -= UpdateMaxCount;
        }
    }
}