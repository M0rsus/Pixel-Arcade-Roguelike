using Demo;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Game/Stats")]
    public class Stats : ScriptableObject
    {
        [Header("Vitality")]
        public StatInt maxHealth;
        public StatFloat healthRegen;
        public StatFloat lifeSteal;
        public StatInt maxArmor;
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

#if UNITY_EDITOR
        void OnValidate()
        {
            if (!Application.isPlaying) return;
            
            maxHealth.Refresh();
            healthRegen.Refresh();
            lifeSteal.Refresh();
            maxArmor.Refresh();
            armorRegen.Refresh();
            regenArmorAtFullHealth.Refresh();
                
            moveSpeed.Refresh();
            rotationSpeed.Refresh();
                
            contactDamage.Refresh();
            contactDamageCooldown.Refresh();
            bulletDamage.Refresh();
            shootCooldown.Refresh();
            bulletSpeed.Refresh();
            lifeTime.Refresh();
            range.Refresh();
            bounces.Refresh();
            bounceOffEnemies.Refresh();
        }
#endif
    }
}