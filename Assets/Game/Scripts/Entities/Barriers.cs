using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class Barriers : MonoBehaviour, IOnUpdateListener
    {
        [SerializeField] 
        private Stats playerStats;
        [SerializeField]
        private UI.SliderView cooldownView;
        [SerializeField] 
        private Color openBarrierColor;
        [SerializeField]
        private Color closedBarrierColor;

        private readonly StatFloat _timer = new(0f);
        
        public static event Action OnBarrierCrossed;
        private CompositeCollider2D _compositeCollider;
        private Tilemap _tilemap;
        private void Awake()
        {
            GameUpdate.Register(this);
            _compositeCollider = GetComponent<CompositeCollider2D>();
            _tilemap = GetComponentInParent<Tilemap>();
            if (cooldownView)
                cooldownView.Initialize(_timer, playerStats.doorCooldown);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            CloseBarrier();
        }

        private void CloseBarrier()
        {
            cooldownView.gameObject.SetActive(true);
            cooldownView.gameObject.transform.SetAsFirstSibling();
            OnBarrierCrossed?.Invoke();
            _timer.Value = playerStats.doorCooldown.GetValue();
            _compositeCollider.isTrigger = false;
            _tilemap.color = closedBarrierColor;
        }

        private void OpenBarrier()
        {
            cooldownView.gameObject.SetActive(false);
            _timer.Value = 0;
            _compositeCollider.isTrigger = true;
            _tilemap.color = openBarrierColor;
        }

        public void OnUpdate(float deltaTime)
        {
            _timer.Value -= deltaTime;
            if (_timer.GetValue() <= 0) OpenBarrier();
        }
    }
}