using Cards;

namespace Game
{
    [System.Serializable]
    public class InventorySlot
    {
        public BaseCard card;

        public InventorySlot(BaseCard card)
        {
            this.card = card;
        }
    }
}