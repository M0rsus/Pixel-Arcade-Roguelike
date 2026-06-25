using Game;
using UnityEngine;

namespace Cards
{
    [System.Serializable]
    public abstract class Card
    {
        public Sprite sprite;
        public string name;
        [TextArea]
        public string description;
        public Rarity rarity;

        public abstract Effect GetEffect();
    }
}