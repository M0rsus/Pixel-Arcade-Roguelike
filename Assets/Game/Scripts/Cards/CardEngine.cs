using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    [System.Serializable]
    public sealed class CardEngine : Card
    {
        public Modifier<float> MovementSpeed { get; private set; }
        public Modifier<float> Cooldown { get; private set; }
        protected override void OnStart()
        {
            MovementSpeed = new Modifier<float>(3f);
            Cooldown = new Modifier<float>(0.5f);
        }

        protected override void UpdateDisplayed()
        {
            Debug.Log($"Bullet Speed: <color=red>{MovementSpeed.Value}</color>");
        }

        protected override void UpdateTaken()
        {
            Debug.Log($"Fire Rate: <color=green>{Cooldown.Value}</color>");
        }

        protected override void CardTaken()
        {
            playerStats.moveSpeed.AddModifier(MovementSpeed);
            playerStats.shootCooldown.AddModifier(Cooldown);
        }

        public override string ItemName() => "Engine";

        public override string Description()
        {
            return $"Movement speed: <color=green>+{MovementSpeed.Value}</color>\n" +
                   $"Cooldown:  <color=red>+{Cooldown.Value}</color>\n";
        }
    }
}