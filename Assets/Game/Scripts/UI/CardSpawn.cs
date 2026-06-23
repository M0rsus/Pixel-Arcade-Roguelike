using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CardSpawn : MonoBehaviour
    {
        
        public void Instantiate(GameObject prefab, GameObject[] cardSpawners)
        {
            Instantiate(prefab, cardSpawners[0].transform);
        }
    }
}