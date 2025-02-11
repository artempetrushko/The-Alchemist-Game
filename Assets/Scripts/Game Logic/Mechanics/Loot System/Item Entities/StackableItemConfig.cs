using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItemConfig : ItemConfig
    {
        [SerializeField] private int _stackMaxCount = 10;

        public int StackMaxCount => _stackMaxCount;
    }
}