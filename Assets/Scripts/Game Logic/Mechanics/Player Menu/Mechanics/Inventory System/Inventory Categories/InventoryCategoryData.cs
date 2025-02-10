using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class InventoryCategoryData
    {
        [SerializeField]
        private Sprite categoryIcon;
        [SerializeField]
        private InventoryCategoryView categoryViewPrefab;

        public Sprite CategoryIcon => categoryIcon;
        public InventoryCategoryView CategoryViewPrefab => categoryViewPrefab;
    }
}