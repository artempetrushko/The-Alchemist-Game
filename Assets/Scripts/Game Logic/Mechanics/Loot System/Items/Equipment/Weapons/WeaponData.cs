using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class WeaponData : EquipmentData
    {
        [Header("Параметры оружия")]
        [SerializeField]
        protected int damage;
        [SerializeField]
        protected float range;
        [SerializeField]
        protected float accuracy;
        [SerializeField]
        protected float attackSpeed;
        [SerializeField]
        protected float cooldownTime;
        [SerializeField]
        protected int penetratingPower;

        public int BaseDamage => damage;
        public float BaseRange => range;
        public float BaseAccuracy => accuracy;
        public float BaseAttackSpeed => attackSpeed;
        public float BaseCooldownTime => cooldownTime;
        public int BasePenetratingPower => penetratingPower;
    }
}