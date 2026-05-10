using System;
using UnityEngine;

namespace Game
{
    public class ButtonClass : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject card;
        public static event Action OnCanvasEnable;
        public static event Action OnCanvasDisable;
        public static event Action OnCardCreate;

        public void EnableCanvas()
        {
            canvas.SetActive(true);
            OnCanvasEnable?.Invoke();
        }

        public void CanvasDisable()
        {
            canvas.SetActive(false);
            OnCanvasDisable?.Invoke();
        }

        public void CreateCard()
        {
            Instantiate(card, spawn.transform);
            OnCardCreate?.Invoke();
        }
    }
}