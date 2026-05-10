using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class PlayerCards : MonoBehaviour, IOnUpdateListener
    {
        public static PlayerCards Instance;
        [SerializeField] [ReadOnly]
        private List<Card> _cards = new List<Card>();

        void Awake()
        {
            Instance = this;
            GameManager.Register(this);
        }

        
        public void OnUpdate(float deltaTime)
        {
            foreach (var card in _cards)
            {
                card.OnUpdate();
            }
        }
        public void AddCard(Card card)
        {
            _cards.Add(card);
            card.CloneCard();
            Debug.Log("<color=green>Card Added</color>");
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }
    }
}