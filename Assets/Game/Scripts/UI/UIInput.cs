using System;
using UnityEngine;

namespace UI
{
    public sealed class UIInput : MonoBehaviour, IPause
    {
        public event Action PauseInput;
        
        public void OnPause()
        {
            PauseInput?.Invoke();
        }
    }
}