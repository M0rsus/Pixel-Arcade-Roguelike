using Game;
using UnityEngine;

namespace Cards
{
    public abstract class BaseCard
    {
        public Sprite cardSprite;
        public string cardName;
        [TextArea]
        public string cardDescription;
        public Rarity cardRarity;

        public abstract Effect GetEffect();
    }
}