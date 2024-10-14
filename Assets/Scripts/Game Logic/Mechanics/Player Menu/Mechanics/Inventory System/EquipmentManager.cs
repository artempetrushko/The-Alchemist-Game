using System;
using Zenject;

namespace GameLogic.PlayerMenu.Inventory
{
    public class EquipmentManager : IDisposable
    {
        private ABC_Controller _playerController;
        private SignalBus _signalBus;
        private ABC_Controller.Weapon _currentWeaponEntity;

        public EquipmentManager(ABC_Controller playerController, SignalBus signalBus)
        {
            _playerController = playerController;
            _signalBus = signalBus;

            _signalBus.Subscribe<WeaponChangedSignal>(OnWeaponChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<WeaponChangedSignal>(OnWeaponChanged);
        }

        private void OnWeaponChanged(WeaponChangedSignal signal)
        {
            _playerController.DisableWeapon(_currentWeaponEntity.weaponID);
            if (signal.Weapon == null)
            {
                _currentWeaponEntity = null;
                return;
            }

            _currentWeaponEntity = signal.Weapon.LinkedABCWeapon;
            _playerController.EnableWeapon(_currentWeaponEntity.weaponID);

            _currentWeaponEntity.weaponGraphics[0].weaponObjMainGraphic.GameObject = signal.Weapon.PhysicalRepresentation.gameObject;
            _currentWeaponEntity.weaponGraphics[0].CreateGraphicObject();
            _currentWeaponEntity.CreateObjectPools();
        }
    }
}