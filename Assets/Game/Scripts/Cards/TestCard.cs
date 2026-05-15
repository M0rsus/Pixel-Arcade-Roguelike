using UnityEngine;

namespace Game
{
    [System.Serializable]
    public sealed class TestCard : Card
    {
        [SerializeField] 
        private BulletComponent bulletPrefab;
        public float BulletSpeed { get; set; }
        public float FireRate { get; set; }
        protected override void OnStart()
        {
            
        }

        protected override void UpdateDisplayed()
        {
            Debug.Log($"Bullet Speed: <color=red>{BulletSpeed}</color>");
        }

        protected override void UpdateTaken()
        {
            Debug.Log($"Fire Rate: <color=green>{FireRate}</color>");
        }
    }
}