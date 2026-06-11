using UnityEngine;

namespace Game
{
    public class Bounds : MonoBehaviour
    {
        public static Bounds Instance { get; private set; }
        
        [SerializeField] 
        private Transform left;
        [SerializeField] 
        private Transform right;
        [SerializeField] 
        private Transform up;
        [SerializeField] 
        private Transform bottom;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public bool InBounds(Vector3 position)
        {
            return left.position.x < position.x && position.x < right.position.x &&
                   up.position.y > position.y && position.y > bottom.position.y;
        }
        
        public float GetMinX() => left.position.x;
        public float GetMaxX() => right.position.x;
        public float GetMinY() => bottom.position.y;
        public float GetMaxY() => up.position.y;
    }
}