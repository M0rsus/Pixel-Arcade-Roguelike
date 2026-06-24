using System;
using Cards;
using Demo;
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
        private TextMeshProUGUI cardName;
        [SerializeField]
        private TextMeshProUGUI description;
        
        private Image _backgroundCard;
        private Outline _outlineCard;

        private void Awake()
        {
            _backgroundCard = GetComponent<Image>();
            _outlineCard = GetComponent<Outline>();
            imageContainer.sprite = card.cardSprite;
            cardName.text = card.cardName;
            description.text = card.cardDescription;
            
            _backgroundCard.color = card.cardRarity.MainBackgroundColor;
            _outlineCard.effectColor = card.cardRarity.MainColor;
            cardName.color = card.cardRarity.MainColor;
            imageBackground.color = card.cardRarity.SecondaryBackgroundColor;
        }
        public void PickCard()
        {
            OnPickedCard?.Invoke(card);
            Destroy(gameObject);
            CardSelected?.Invoke();
        }
    }
}