using UnityEngine;

namespace UI
{
    public class CardSelectionView : MonoBehaviour
    {
        [SerializeField]
        private CardSpawnManager[] cardSpawnerManager;
        public void Show()
        {
            gameObject.SetActive(true);
            foreach (CardSpawnManager manager in cardSpawnerManager)
                manager.SpawnCard();
        }

        public void Hide()
        {
            foreach (CardSpawnManager manager in cardSpawnerManager)
                manager.DeleteAllCards();
            gameObject.SetActive(false);
        }
    }
}