using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnStartWave;
        public static event Action OnEndWave;

        private void Start()
        {
            StartWave();
        }
        public void StartWave()
        {
            OnStartWave?.Invoke();
        }

        public void EndWave()
        {
            OnEndWave?.Invoke();
            Debug.Log("End wave");
        }
        
        public void StartCardSelection()
        {
            
        }
        
        public void EndCardSelection()
        {
            
        }

        private void CheckProgressWave(int oldCount, int newCount)
        {
            if (newCount <= 0)
                EndWave();
        }

        private void OnEnable()
        {
            LevelContext.Instance.currentEnemyCount.OnChanged += CheckProgressWave;
        }

        private void OnDisable()
        {
            LevelContext.Instance.currentEnemyCount.OnChanged -= CheckProgressWave;
        }
    }
}