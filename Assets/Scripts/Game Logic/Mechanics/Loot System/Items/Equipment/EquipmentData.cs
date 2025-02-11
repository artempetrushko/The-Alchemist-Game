using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentData : ItemData
    {
        [Header("��������� ����������")]
        [SerializeField]
        protected Sprite bigIcon;
        [SerializeField]
        protected int endurance;

        public Sprite BigIcon => bigIcon;
        public int BaseEndurance => endurance;
    }
}