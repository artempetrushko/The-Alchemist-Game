using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    [Serializable]
    public class RecipeItem
    {
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private int _count;

        public ItemConfig ItemConfig => _itemConfig;
        public int Count => _count;
    }
}