using Cards;
using Demo;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardVieww : MonoBehaviour
    {
        [SerializeField]
        private EntityInventory inventory;
        [SerializeReference] [SRDemo]
        private BaseCard card;
        [SerializeField]
        private Image imageBackground;
        [SerializeField]
        private Image imageContainer;
        [SerializeField]
        private Outline outline;
        [SerializeField]
        private TextMeshProUGUI cardName;
        [SerializeField]
        private TextMeshProUGUI description;

        private void Awake()
        {
            imageContainer.sprite = card.cardSprite;
            cardName.text = card.cardName;
            description.text = card.cardDescription;
            
            outline.effectColor = card.cardRarity.MainColor;
            cardName.color = card.cardRarity.MainColor;
            imageContainer.color = card.cardRarity.MainBackgroundColor;
            imageBackground.color = card.cardRarity.SecondaryBackgroundColor;
        }
        public void PickCard()
        {
            inventory.AddCard(card);
            Destroy(gameObject);
        }
    }
}