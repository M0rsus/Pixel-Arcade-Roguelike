using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AIChase
    {
        [SerializeField]
        private Transform playerPosition;
        [SerializeField]
        private Transform enemyPosition;
        [SerializeField]
        private float distanceFromPlayer;
        
        public Vector2 Direction { get; private set; }

        public void OnUpdate(float deltaTime)
        {
            Direction = playerPosition.position - enemyPosition.position;
        }
    }
}