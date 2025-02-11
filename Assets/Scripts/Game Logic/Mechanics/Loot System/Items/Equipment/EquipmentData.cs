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

        public Sprite BigIcon => bigIcon;
        public int BaseEndurance => endurance;
    }
}