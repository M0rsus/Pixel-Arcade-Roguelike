using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EntityRarities : MonoBehaviour
    {
        [SerializeField]
        private List<Rarity> rarities;
        
        public List<Rarity> Rarities => rarities;

        public int GetTotalWeight()
        {
            int total = 0;
            foreach (Rarity rarity in rarities)
                total += rarity.Weight;
            
            return total;
        }
    }
}