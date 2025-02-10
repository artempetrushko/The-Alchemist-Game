using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItemConfig : ItemConfig
    {
        [SerializeField] private int count = 1;
        [SerializeField] private int stackMaxCount = 10;

        public int BaseCount => count;
        public int StackMaxCount => stackMaxCount;
    }
}