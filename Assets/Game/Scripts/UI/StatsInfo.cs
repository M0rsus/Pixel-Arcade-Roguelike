using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Game;
using UnityEngine;

namespace UI
{
    public class StatsInfo : MonoBehaviour
    {
        [SerializeField]
        private Stats playerStats;
        [SerializedDictionary("Stat Name", "Config")]
        public SerializedDictionary<StatName, StatInfo> statsInfo;

        private Dictionary<StatName, StatType> _statsType;
        
        private Dictionary<StatName, StatInt> _statsInt;
        private Dictionary<StatName, StatFloat> _statsFloat;
        private Dictionary<StatName, StatBool> _statsBool;
        
        // Vitality
        private StatInt _maxHealth;
        private StatFloat _healthRegen;
        private StatFloat _lifeSteal;
        private StatInt _maxArmor;
        private StatFloat _armorRegen;
        private StatBool _regenArmorAtFullHealth;
        
        // Movement
        private StatFloat _moveSpeed;
        private StatFloat _rotationSpeed;
        
        // Power
        private StatInt _contactDamage;
        private StatFloat _contactDamageCooldown;
        private StatInt _bulletDamage;
        private StatFloat _shootCooldown;
        private StatFloat _bulletSpeed;
        private StatFloat _lifeTime;
        private StatFloat _range;
        private StatInt _bounces;
        private StatBool _bounceOffEnemies;
        private void Awake()
        {
            _maxHealth = playerStats.maxHealth;
            _healthRegen = playerStats.healthRegen;
            _lifeSteal = playerStats.lifeSteal;
            _maxArmor = playerStats.maxArmor;
            _armorRegen = playerStats.armorRegen;
            _regenArmorAtFullHealth = playerStats.regenArmorAtFullHealth;
            
            _moveSpeed = playerStats.moveSpeed;
            _rotationSpeed = playerStats.rotationSpeed;
            
            _contactDamage = playerStats.contactDamage;
            _contactDamageCooldown = playerStats.contactDamageCooldown;
            _bulletDamage = playerStats.bulletDamage;
            _shootCooldown = playerStats.shootCooldown;
            _bulletSpeed = playerStats.bulletSpeed;
            _lifeTime = playerStats.lifeTime;
            _range = playerStats.range;
            _bounces = playerStats.bounces;
            _bounceOffEnemies = playerStats.bounceOffEnemies;
            
            _statsInt = new Dictionary<StatName, StatInt>()
            {
                { StatName.MaxHp, _maxHealth },
                { StatName.MaxArmor, _maxArmor },
                { StatName.ContactDamage, _contactDamage },
                { StatName.BulletDamage, _bulletDamage },
                { StatName.Bounces, _bounces }
            };
            _statsFloat = new Dictionary<StatName, StatFloat>()
            {
                { StatName.HealthRegen, _healthRegen },
                { StatName.LifeSteal, _lifeSteal },
                { StatName.ArmorRegen, _armorRegen },
                { StatName.MoveSpeed, _moveSpeed },
                { StatName.RotationSpeed, _rotationSpeed },
                { StatName.ContactDamageCooldown, _contactDamageCooldown },
                { StatName.ShootCooldown, _shootCooldown},
                { StatName.BulletSpeed, _bulletSpeed },
                { StatName.LifeTime, _lifeTime },
                { StatName.Range, _range }
            };
            _statsBool = new Dictionary<StatName, StatBool>()
            {
                { StatName.RegenArmorAtFullHealth, _regenArmorAtFullHealth },
                { StatName.BounceOffEnemies, _bounceOffEnemies }
            };
            _statsType = new Dictionary<StatName, StatType>()
            {
                { StatName.MaxHp, StatType.Int},
                { StatName.HealthRegen, StatType.Float},
                { StatName.LifeSteal, StatType.Float},
                { StatName.MaxArmor, StatType.Int},
                { StatName.ArmorRegen, StatType.Float},
                { StatName.RegenArmorAtFullHealth, StatType.Bool },
                { StatName.MoveSpeed, StatType.Float },
                { StatName.RotationSpeed, StatType.Float},
                { StatName.ContactDamage, StatType.Int},
                { StatName.ContactDamageCooldown, StatType.Float},
                { StatName.BulletDamage, StatType.Int},
                { StatName.ShootCooldown, StatType.Float},
                { StatName.BulletSpeed, StatType.Float},
                { StatName.LifeTime, StatType.Float},
                { StatName.Range, StatType.Float},
                { StatName.Bounces, StatType.Int},
                { StatName.BounceOffEnemies, StatType.Bool}
            };
            
            foreach (var (statName, statInfo) in statsInfo)
            {
                var statType = _statsType[statName];
                switch (statType)
                {
                    case StatType.Int:
                        if (statInfo.statView)
                            statInfo.statView.Initialize(
                                statInfo.statSprite,
                                statInfo.statName,
                                _statsInt[statName]);
                        break;
                    case StatType.Float:
                        if (statInfo.statView)
                            statInfo.statView.Initialize(
                                statInfo.statSprite,
                                statInfo.statName,
                                _statsFloat[statName]);
                        break;
                    case StatType.Bool:
                        if (statInfo.statView)
                            statInfo.statView.Initialize(
                                statInfo.statSprite,
                                statInfo.statName,
                                _statsBool[statName]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Start()
        {

        }

        public enum StatName
        {
            MaxHp,
            HealthRegen,
            LifeSteal,
            MaxArmor,
            ArmorRegen,
            RegenArmorAtFullHealth,
            MoveSpeed,
            RotationSpeed,
            ContactDamage,
            ContactDamageCooldown,
            BulletDamage,
            ShootCooldown,
            BulletSpeed,
            LifeTime,
            Range,
            Bounces,
            BounceOffEnemies
        }

        private enum StatType
        {
            Int,
            Float,
            Bool
        }
    }
}