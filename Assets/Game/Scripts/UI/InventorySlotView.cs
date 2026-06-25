using Cards;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] 
        private Image icon;

        public void DisplayItem(Card card)
        {
            if (card == null || card.sprite == null)
            {
                ClearItem();
                return;
            }
            icon.sprite = card.sprite;
            icon.gameObject.SetActive(true);
        }

        private void ClearItem()
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }
    }
}