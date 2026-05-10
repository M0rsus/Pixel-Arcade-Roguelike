using System.Collections;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Card
    {
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
        protected virtual void OnStart() {}

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

        protected virtual void UpdateDisplayed() {}
        protected virtual void UpdateTaken() {}

        public void OnPick()
        {
            _state = CardState.Taken;
        }
        public virtual void OnStartWave() {}
        public virtual void OnEndWave() {}
        
        private enum CardState
        {
            Created,
            Displayed,
            Taken
        }
    }
}