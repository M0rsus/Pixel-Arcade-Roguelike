using Demo;
using UnityEngine;

namespace Game
{
    public class CardView : MonoBehaviour, IOnUpdateListener
    {
        PlayerCards _playerCards;
        [SerializeReference] [SRDemo]
        private Card card;

        void Awake()
        {
            _playerCards = PlayerCards.Instance;
            GameManager.Register(this);
            card.OnCreate();
        }

        public void OnUpdate(float deltaTime)
        {
            card.OnUpdate();
        }
        public void PickCard()
        {
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            card.OnPick();
            _playerCards.AddCard(card.CloneCard());
        }
    }
}