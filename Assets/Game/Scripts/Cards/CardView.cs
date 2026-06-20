using Demo;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardView : MonoBehaviour, IOnUpdateListener
    {
        [SerializeReference] [SRDemo]
        private Card card;
        [SerializeField]
        private Image imageContainer;
        [SerializeField]
        private TextMeshProUGUI itemName;
        [SerializeField]
        private TextMeshProUGUI description;

        private void Awake()
        {
            GameUpdate.Register(this);
            card.OnCreate();
            imageContainer.sprite = card.ItemImage();
            itemName.text = card.ItemName();
            description.text = card.Description();
        }

        public void OnUpdate(float deltaTime)
        {
            card.OnUpdate();
        }
        public void PickCard()
        {
            card.OnPick();
            PlayerCards.Instance.AddCard(card.CloneCard());
            Destroy(gameObject);
        }

        public void RemoveCard()
        {
            card.RemoveCard();
        }

        private void OnDestroy()
        {
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