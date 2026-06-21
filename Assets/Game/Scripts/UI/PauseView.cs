using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;
        
        private bool _paused;
        public void PressPause()
        {
            if (_paused)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1;
                playerInput.SwitchCurrentActionMap("Player");
                _paused = false;
            }
            else
            {
                gameObject.SetActive(true);
                Time.timeScale = 0;
                playerInput.SwitchCurrentActionMap("UI");
                _paused = true;
            }
        }
    }
}