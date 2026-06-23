using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Rarity
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
    }
}