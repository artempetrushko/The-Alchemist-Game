using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentConfig : ItemConfig
    {
        [Header("��������� ����������")]
        [SerializeField] protected Sprite bigIcon;
        [SerializeField] protected int endurance;
        [SerializeField] protected int maxRuneSize;
        [SerializeField] protected int poweredEnergyCount;
        [SerializeField] protected int energyCapacity;
        [SerializeField] private List<AspectConfig> containedAspects = new();

        public Sprite BigIcon => bigIcon;
        public int BaseEndurance => endurance;
        public int BaseMaxRuneSize => maxRuneSize;
        public int BasePoweredEnergyCount => poweredEnergyCount;
        public int BaseEnergyCapacity => energyCapacity;
    }
}