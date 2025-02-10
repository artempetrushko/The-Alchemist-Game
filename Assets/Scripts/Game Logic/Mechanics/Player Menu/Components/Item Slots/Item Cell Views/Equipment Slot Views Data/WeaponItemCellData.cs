using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class WeaponItemCellData
    {
        [SerializeField]
        private WeaponHandPosition weaponHandPosition;
        [SerializeField]
        private ItemCellView weaponCellView;

        public WeaponHandPosition WeaponHandPosition => weaponHandPosition;
        public ItemCellView WeaponCellView => weaponCellView;
    }
}