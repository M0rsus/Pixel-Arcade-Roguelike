using Game;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField]
        private HUDView hudView;
        [SerializeField]
        private CardSelectionView cardSelectionView;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            hudView.Show();
            cardSelectionView.Hide();
        }

        private void StartSelectingCard()
        {
            cardSelectionView.Show();
            hudView.Hide();
        }

        private void EndSelectingCard()
        {
            hudView.Show();
            cardSelectionView.Hide();
        }

        private void OnEnable()
        {
            GameManager.OnStartCardSelection += StartSelectingCard;
            GameManager.OnEndCardSelection += EndSelectingCard;
        }

        private void OnDisable()
        {
            GameManager.OnStartCardSelection -= StartSelectingCard;
            GameManager.OnEndCardSelection -= EndSelectingCard;
        }
    }
}