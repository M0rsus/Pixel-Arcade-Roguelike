using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace UI
{
    public class CardSpawn : MonoBehaviour
    {
        [SerializeField] 
        private List<GameObject> cardSpawners;
        public void Instantiate(GameObject prefab, GameObject[] cardSpawners)
        {
            Instantiate(CardRegistry.Cards[0], cardSpawners[0].transform);
        }
    }
}