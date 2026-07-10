using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageRotator : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private StatFloat duration;
        
        private readonly AsyncTimer _asyncTimer = new();
        private readonly StatFloat _timer = new();
        private int _counter;

        private void OnEnable()
        {
            image.sprite = sprites[_counter];
            _asyncTimer.Start(duration, CurrentTimer);
        }

        private void CurrentTimer(StatFloat timer)
        {
            _timer.Value = timer.Value;
            if (_timer.Value < duration.Value) return;
            ChangeImage();
            _asyncTimer.Start(duration, CurrentTimer);
        }

        private void ChangeImage()
        {
            _counter = (_counter + 1) % sprites.Length;
            image.sprite = sprites[_counter];
        }
    }
}