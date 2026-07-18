using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Game/Stats")]
    public class Stats : ScriptableObject
    {
        [Header("Vitality")]
        public StatFloat maxHealth;
        public StatFloat healthRegen;
        public StatFloat lifeSteal;
        public StatFloat maxArmor;
        public StatFloat armorRegen;
        public StatBool regenArmorAtFullHealth;
        
        [Header("Movement")]
        public StatFloat moveSpeed;
        public StatFloat rotationSpeed;
        
        [Header("Power")]
        public StatInt contactDamage;
        public StatFloat contactDamageCooldown;
        public StatInt bulletDamage;
        public StatFloat shootCooldown;
        public StatFloat bulletSpeed;
        public StatFloat lifeTime;
        public StatFloat range;
        public StatInt bounces;
        public StatBool bounceOffEnemies;
        public StatFloat spread;
        public StatInt bullets;
        
        [Header("Objects")]
        public StatFloat barriersCooldown;
    }
}