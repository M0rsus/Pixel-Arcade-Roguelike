using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private Tilemap spawnerTilemap;
        
        private readonly List<Vector3> _spawnPoint = new List<Vector3>();

        private void Start()
        {
            Grid grid = spawnerTilemap.layoutGrid;
            
            foreach (var pos in spawnerTilemap.cellBounds.allPositionsWithin)
            {
                if (!spawnerTilemap.HasTile(pos)) continue;
                
                Vector3 worldPos = grid.CellToWorld(pos);
                worldPos += grid.cellSize * 0.5f; 
                _spawnPoint.Add(worldPos);
            }
        }
        public void Instantiate(GameObject prefab)
        {
            Instantiate(prefab, _spawnPoint[Random.Range(0, _spawnPoint.Count)], Quaternion.identity);
        }
    }
}