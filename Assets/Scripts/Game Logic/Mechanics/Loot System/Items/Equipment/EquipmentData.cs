using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentData : ItemData
    {
        [Header("Параметры снаряжения")]
        [SerializeField]
        protected Sprite bigIcon;
        [SerializeField]
        protected int endurance;
        [SerializeField]
        protected int maxRuneSize;
        [SerializeField]
        protected int poweredEnergyCount;
        [SerializeField]
        protected int energyCapacity;

        public Sprite BigIcon => bigIcon;
        public int BaseEndurance => endurance;
        public int BaseMaxRuneSize => maxRuneSize;
        public int BasePoweredEnergyCount => poweredEnergyCount;
        public int BaseEnergyCapacity => energyCapacity;
    }
}