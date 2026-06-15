using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FollowTargetUI : MonoBehaviour
    {
        [SerializeField] 
        private Slider slider;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private Camera mainCamera;

        [SerializeField] 
        private float offsetY;

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 worldPosition = target.position + Vector3.up * offsetY;

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z < 0)
            {
                slider.gameObject.SetActive(false);
                return;
            }

            slider.gameObject.SetActive(true);

            slider.transform.position = screenPosition;
        }
    }
}