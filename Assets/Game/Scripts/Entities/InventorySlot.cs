using Cards;

namespace Game
{
    [System.Serializable]
    public class InventorySlot
    {
        public Card card;

        public InventorySlot(Card card)
        {
            this.card = card;
        }
    }
}