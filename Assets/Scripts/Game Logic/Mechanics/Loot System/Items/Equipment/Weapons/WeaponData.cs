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
        [SerializeField]
        protected bool isBlocking;
        [SerializeField]
        protected bool isCombable;
        [SerializeField]
        protected AnimatorOverrideController animations;

        public bool IsBlocking => isBlocking;
        public bool IsCombable => isCombable;
        public int BaseDamage => damage;
        public float BaseRange => range;
        public float BaseAccuracy => accuracy;
        public float BaseAttackSpeed => attackSpeed;
        public float BaseCooldownTime => cooldownTime;
        public int BasePenetratingPower => penetratingPower;
        public AnimatorOverrideController Animations => animations;
    }
}