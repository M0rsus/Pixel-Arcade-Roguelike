using System;
using Cards;
using Demo;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardView : MonoBehaviour
    {
        public static event Action<Card> OnPickedCard;
        public static event Action CardSelected;
        
        [SerializeReference] [SRDemo]
        private Card card;
        [SerializeField]
        private Image imageBackground;
        [SerializeField]
        private Image imageContainer;
        [SerializeField]
        private TextMeshProUGUI nameContainer;
        [SerializeField]
        private TextMeshProUGUI descriptionContainer;
        
        private Image _backgroundCard;
        private Outline _outlineCard;
        
        public Card Card => card;

        private void Awake()
        {
            _backgroundCard = GetComponent<Image>();
            _outlineCard = GetComponent<Outline>();
            imageContainer.sprite = card.sprite;
            nameContainer.text = card.name;
            descriptionContainer.text = card.description;
            
            _backgroundCard.color = card.rarity.MainBackgroundColor;
            _outlineCard.effectColor = card.rarity.MainColor;
            nameContainer.color = card.rarity.MainColor;
            imageBackground.color = card.rarity.SecondaryBackgroundColor;
        }
        public void PickCard()
        {
            OnPickedCard?.Invoke(card);
            Destroy(gameObject);
            CardSelected?.Invoke();
            Statistics.CardPickedUp();
        }
    }
}