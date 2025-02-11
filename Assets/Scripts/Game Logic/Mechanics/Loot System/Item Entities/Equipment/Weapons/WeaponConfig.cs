using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class WeaponConfig : EquipmentConfig
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _range;
        [SerializeField] private float _accuracy;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private int _penetratingPower;

        public int BaseDamage => _damage;
        public float BaseRange => _range;
        public float BaseAccuracy => _accuracy;
        public float BaseAttackSpeed => _attackSpeed;
        public float BaseCooldownTime => _cooldownTime;
        public int BasePenetratingPower => _penetratingPower;
    }
}