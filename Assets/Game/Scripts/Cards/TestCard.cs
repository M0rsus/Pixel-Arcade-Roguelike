using UnityEngine;

namespace Game
{
    [System.Serializable]
    public sealed class TestCard : Card
    {
        public float BulletSpeed { get; set; }
        public float FireRate { get; set; }
        protected override void OnStart()
        {
            BulletSpeed = 50f;
            FireRate = 30f;
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