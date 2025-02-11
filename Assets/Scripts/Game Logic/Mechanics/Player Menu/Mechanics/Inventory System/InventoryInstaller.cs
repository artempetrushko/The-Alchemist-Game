using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu.Inventory
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryConfig _config;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private HUDEquipmentView _hudEquipmentView;

        public override void InstallBindings()
        {
            Container.Bind<InventoryPresenter>().AsSingle().NonLazy();
            Container.Bind<InventoryView>().FromInstance(_inventoryView).AsSingle().NonLazy();
            Container.Bind<InventoryConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
            Container.Bind<HUDEquipmentView>().FromInstance(_hudEquipmentView).AsSingle().NonLazy();
        }
    }
}