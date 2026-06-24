using Game;
using UnityEngine;

namespace Cards
{
    [System.Serializable]
    public abstract class Card
    {
        public Sprite cardSprite;
        public string cardName;
        [TextArea]
        public string cardDescription;
        public Rarity cardRarity;

        public abstract Effect GetEffect();
    }
}