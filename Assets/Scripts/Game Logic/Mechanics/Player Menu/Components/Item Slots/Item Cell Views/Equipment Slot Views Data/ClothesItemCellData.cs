using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class ClothesItemCellData
    {
        [SerializeField]
        private ClothesType clothesType;
        [SerializeField]
        private ItemCellView clothesCellView;

        public ClothesType ClothesType => clothesType;
        public ItemCellView ClothesCellView => clothesCellView;
    }
}