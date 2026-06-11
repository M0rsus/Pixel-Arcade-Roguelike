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
        }
        
        public void StartCardSelection()
        {
            
        }
        
        public void EndCardSelection()
        {
            
        }
    }
}