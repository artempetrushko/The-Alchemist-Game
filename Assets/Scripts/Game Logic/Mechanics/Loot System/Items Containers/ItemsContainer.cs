using System.Collections.Generic;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainer : InteractiveObject
    {
        [SerializeField] private ItemsSpawnChancesTable _spawnChancesTable;
        [SerializeField] private GameObject _filledContainerEffect;

        private List<ItemState> _containedItems = new();

        public List<ItemState> ContainedItems
        {
            get
            {
                if (_containedItems.Count == 0)
                {
                    _containedItems = ItemsSpawnManager.SpawnItems(_spawnChancesTable);
                }
                return _containedItems;
            }
        }

        public void SetFilledContainerEffectActive(bool isActive)
        {
            if (_filledContainerEffect != null)
            {
                _filledContainerEffect.SetActive(isActive);
            }
        }

        private void OnEnable()
        {
            SetFilledContainerEffectActive(true);
        }
    }
}