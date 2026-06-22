using Game;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField] 
        private UIInput uiInput;
        [SerializeField]
        private HUDView hudView;
        [SerializeField]
        private PauseView pauseView;
        [SerializeField]
        private CardSelectionView cardSelectionView;

        private IPause _pause;

        private void Awake()
        {
            Instance = this;
            _pause = uiInput;
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

        private void Pause()
        {
            pauseView.PressPause();
        }

        private void OnEnable()
        {
            GameManager.OnStartCardSelection += StartSelectingCard;
            GameManager.OnEndCardSelection += EndSelectingCard;
            _pause.PauseInput += Pause;
        }

        private void OnDisable()
        {
            GameManager.OnStartCardSelection -= StartSelectingCard;
            GameManager.OnEndCardSelection -= EndSelectingCard;
            _pause.PauseInput -= Pause;
        }
    }
}