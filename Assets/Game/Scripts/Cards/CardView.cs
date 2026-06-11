using Demo;
using UnityEngine;

namespace Game
{
    public class CardView : MonoBehaviour, IOnUpdateListener
    {
        PlayerCards _playerCards;
        [SerializeReference] [SRDemo]
        private Card card;

        private void Awake()
        {
            _playerCards = PlayerCards.Instance;
            GameUpdate.Register(this);
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

        private void OnDestroy()
        {
            card.OnPick();
            _playerCards.AddCard(card.CloneCard());
            GameUpdate.Unregister(this);
        }

        private void OnEnable()
        {
            card.OnEnable();
        }

        private void OnDisable()
        {
            card.OnDisable();
        }
    }
}