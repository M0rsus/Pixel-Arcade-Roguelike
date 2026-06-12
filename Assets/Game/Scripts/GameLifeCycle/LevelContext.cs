using UnityEngine;

namespace Game
{
    public class LevelContext : MonoBehaviour
    {
        public static LevelContext Instance { get; private set; }
        
        [field: SerializeField] 
        public Bounds LevelBounds { get; private set; }
        
        [field: SerializeField] 
        public Transform PlayerTransform { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}