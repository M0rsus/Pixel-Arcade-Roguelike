using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class EnemySpawnOption
    {
        [Min(0)]
        public int points;
        [Min(0)]
        public int weight;
        public GameObject prefab;
    }
}