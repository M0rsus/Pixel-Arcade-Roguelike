using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Game/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField]
        private string rarityName;
        [SerializeField] [Range(0,10000)]
        private int weight;
        [SerializeField] 
        private Color mainColor;
        [SerializeField]
        private Color mainBackgroundColor;
        [SerializeField] 
        private Color secondaryBackgroundColor;
        
        public Color MainColor => mainColor;
        public Color MainBackgroundColor => mainBackgroundColor;
        public Color SecondaryBackgroundColor => secondaryBackgroundColor;
    }
}