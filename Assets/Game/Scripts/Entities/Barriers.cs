using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class Barriers : MonoBehaviour
    {
        [SerializeField] 
        private Stats playerStats;
        [SerializeField]
        private UI.SliderView cooldownView;
        [SerializeField] 
        private Color openBarrierColor;
        [SerializeField]
        private Color closedBarrierColor;
        private readonly AsyncTimer _asyncTimer = new AsyncTimer();
        private readonly StatFloat _timer = new StatFloat(float.MaxValue);
        
        public static event Action OnBarrierCrossed;
        private CompositeCollider2D _compositeCollider;
        private Tilemap _tilemap;
        private void Awake()
        {
            _compositeCollider = GetComponent<CompositeCollider2D>();
            _tilemap = GetComponentInParent<Tilemap>();
            if (!cooldownView) return;
            cooldownView.Initialize(_timer, playerStats.doorCooldown);
            cooldownView.gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            CloseBarrier();
        }

        private void CloseBarrier()
        {
            _timer.Value = 0;
            cooldownView.gameObject.SetActive(true);
            cooldownView.gameObject.transform.SetAsFirstSibling();
            OnBarrierCrossed?.Invoke();
            _asyncTimer.Start(playerStats.doorCooldown, CurrentCooldown);
            _compositeCollider.isTrigger = false;
            _tilemap.color = closedBarrierColor;
        }

        private void CurrentCooldown(StatFloat timer)
        {
            _timer.Value = timer.Value;
            if (_timer.Value >= playerStats.doorCooldown.GetValue()) OpenBarrier();
        }

        private void OpenBarrier()
        {
            cooldownView.gameObject.SetActive(false);
            _compositeCollider.isTrigger = true;
            _tilemap.color = openBarrierColor;
        }
    }
}