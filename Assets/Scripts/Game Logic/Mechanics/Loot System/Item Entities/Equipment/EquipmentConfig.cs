using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentConfig : ItemConfig
    {
        [SerializeField] protected Sprite bigIcon;
        [SerializeField] protected int endurance;
        [SerializeField] protected int maxRuneSize;
        [SerializeField] protected int poweredEnergyCount;
        [SerializeField] protected int energyCapacity;
    }
}