using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItemData : ItemData
    {
        [Header("��������� ���������� ��������")]
        [SerializeField]
        protected int count = 1;
        [SerializeField]
        protected int stackMaxCount = 10;

        public int BaseCount => count;
        public int StackMaxCount => stackMaxCount;
    }
}