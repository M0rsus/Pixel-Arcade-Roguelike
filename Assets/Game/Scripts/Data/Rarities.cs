using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Rarities", menuName = "Game/Rarities")]
    public class Rarities : ScriptableObject
    {
        public Rarity commonRarity;
        public Rarity uncommonRarity;
        public Rarity rareRarity;
        public Rarity epicRarity;
        public Rarity legendaryRarity;
    }
}