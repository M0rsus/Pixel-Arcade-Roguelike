using System;
using Game;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class Card
    {
        public static event Action OnPicked;
        [SerializeField] 
        protected Stats playerStats;
        [SerializeField] 
        protected Sprite itemImage;
        
        private CardState _state;
        public Card CloneCard()
        {
            return (Card)MemberwiseClone();
        }

        public void OnCreate()
        {
            _state = CardState.Created;
            Debug.Log("Card Created");
            OnStart();
            _state = CardState.Displayed;
        }
        protected virtual void OnStart() { }

        public void OnUpdate()
        {
            Debug.Log($"State: <color=yellow>{_state}</color>");
            switch (_state)
            {
                case CardState.Displayed:
                    UpdateDisplayed();
                    break;
                case CardState.Taken:
                    UpdateTaken();
                    break;
            }
        }

        protected virtual void UpdateDisplayed() { }
        protected virtual void UpdateTaken() { }

        public void OnPick()
        {
            _state = CardState.Taken;
            CardTaken();
            OnPicked?.Invoke();
        }

        protected virtual void CardTaken() { }
        public virtual void RemoveCard() { }
        public virtual void OnStartWave() { }
        public virtual void OnEndWave(float delay) { }

        public void OnEnable()
        {
            GameManager.OnStartWave += OnStartWave;
            GameManager.OnEndWave += OnEndWave;
        }

        public void OnDisable()
        {
            GameManager.OnStartWave -= OnStartWave;
            GameManager.OnEndWave -= OnEndWave;
        }

        public virtual Sprite ItemImage() => itemImage;
        public virtual string ItemName() => "";
        public virtual string Description() => "";
        
        private enum CardState
        {
            Created,
            Displayed,
            Taken
        }
    }
}