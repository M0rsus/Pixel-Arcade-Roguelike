using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Game/Wave")]
    public class WaveData : ScriptableObject
    {
        [Min(0)]
        public int points;
        public List<EnemySpawnOption> enemySpawnOptions;
    }
}