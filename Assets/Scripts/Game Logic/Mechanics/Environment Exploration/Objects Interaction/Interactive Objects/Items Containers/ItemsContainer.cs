using System.Collections.Generic;
using GameLogic.LootSystem;
using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainer : InteractiveObject
    {
        [SerializeField] private ItemsSpawnChancesTable _spawnChancesTable;
        [SerializeField] private GameObject _filledContainerEffect;

        private ItemsContainerMenuPresenter _itemsContainerMenuPresenter;
        private List<Item> _containedItems = new();

        public List<Item> ContainedItems
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

        [Inject]
        public void Construct(ItemsContainerMenuPresenter itemsContainerMenuPresenter)
        {
            _itemsContainerMenuPresenter = itemsContainerMenuPresenter;
        }

        public override void Interact() => _itemsContainerMenuPresenter.Show(this);

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