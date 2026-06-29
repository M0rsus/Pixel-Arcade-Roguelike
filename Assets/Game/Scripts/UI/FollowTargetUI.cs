using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FollowTargetUI : MonoBehaviour, IOnLateUpdateListener
    {
        [SerializeField] 
        private GameObject UIObject;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Camera mainCamera;

        [SerializeField] 
        private float offsetY;

        private void OnEnable()
        {
            GameUpdate.Register(this);
        }

        private void OnDisable()
        {
            GameUpdate.Unregister(this);
        }
        public void OnLateUpdate(float deltaTime)
        {
            if (target == null) return;

            Vector3 worldPosition = target.position + Vector3.up * offsetY;

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z < 0)
            {
                UIObject.gameObject.SetActive(false);
                return;
            }

            UIObject.gameObject.SetActive(true);

            UIObject.transform.position = screenPosition;
        }
    }
}