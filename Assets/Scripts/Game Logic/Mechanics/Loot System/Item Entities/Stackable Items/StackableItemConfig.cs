using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItemConfig : ItemConfig
    {
        [Header("��������� ���������� ��������")]
        [SerializeField] protected int count = 1;
        [SerializeField] protected int stackMaxCount = 10;

        public int BaseCount => count;
        public int StackMaxCount => stackMaxCount;
    }
}