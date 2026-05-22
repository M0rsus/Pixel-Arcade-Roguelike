using UnityEngine;

namespace Game
{
    public class Bounds : MonoBehaviour
    {
        [SerializeField] 
        private Transform left;
        [SerializeField] 
        private Transform right;
        [SerializeField] 
        private Transform up;
        [SerializeField] 
        private Transform bottom;

        public bool InBounds(Vector3 position)
        {
            return left.position.x < position.x && position.x < right.position.x &&
                   up.position.y > position.y && position.y > bottom.position.y;
        }
    }
}