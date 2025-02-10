using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class EquipmentItemCellData
    {
        [SerializeField]
        private WeaponItemCellData[] weaponItemCellDatas;
        [SerializeField]
        private ClothesItemCellData[] clothesItemCellDatas;

        public WeaponItemCellData[] WeaponItemCellDatas => weaponItemCellDatas;
        public ClothesItemCellData[] ClothesItemCellDatas => clothesItemCellDatas;
    }
}