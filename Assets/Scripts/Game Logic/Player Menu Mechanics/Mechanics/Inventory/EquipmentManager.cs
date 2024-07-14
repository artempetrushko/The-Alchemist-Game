using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentManager
{
    private ABC_Controller _playerController;

    private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) _leftWeapon;
    private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) _rightWeapon;
    private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) _currentWeapon;

    public EquipmentManager(ABC_Controller playerController)
    {
        _playerController = playerController;
    }

    public void SelectLeftHandWeapon(InputAction.CallbackContext context) => SelectWeapon(context, WeaponHandPosition.Left);

    public void SelectRightHandWeapon(InputAction.CallbackContext context) => SelectWeapon(context, WeaponHandPosition.Right);

    public void SelectEquippedWeapon(WeaponHandPosition weaponHandPosition)
    {
        if (_currentWeapon != (null, null))
        {
            _playerController.DisableWeapon(_currentWeapon.entity.weaponID);
        }
        _currentWeapon = GetWeaponByHandPosition(weaponHandPosition);  
        if (_currentWeapon != (null, null))
        {
            _playerController.EnableWeapon(_currentWeapon.entity.weaponID);
            UpdateWeaponGraphics(_currentWeapon);
        }
    }

    public void SubscribeWeaponItemDatas(WeaponItemSlot[] weaponItemDatas)
    {
        foreach (var weaponItemData in weaponItemDatas)
        {
            weaponItemData.WeaponStateChanged += SetEquippedWeaponData;
        }
    }

    public void UnsubscribeWeaponItemDatas(WeaponItemSlot[] weaponItemDatas)
    {
        foreach (var weaponItemData in weaponItemDatas)
        {
            weaponItemData.WeaponStateChanged -= SetEquippedWeaponData;
        }
    }

    public void SetEquippedWeaponData(WeaponState weaponState, WeaponHandPosition handPosition)
    {
        var weapon = GetWeaponByHandPosition(handPosition);
        if (weaponState != null)
        {
            weapon = (FindWeaponEntity(weaponState), weaponState.BaseParams.PhysicalRepresentation.gameObject);
            if (_currentWeapon == weapon)
            {
                _playerController.EnableWeapon(weapon.entity.weaponID);
                UpdateWeaponGraphics(weapon);
            }
        }
        else
        {
            _playerController.DisableWeapon(weapon.entity.weaponID);
            weapon = (null, null);
        }
    }

    private void SelectWeapon(InputAction.CallbackContext context, WeaponHandPosition weaponHandPosition)
    {
        if (context.performed)
        {
            SelectEquippedWeapon(weaponHandPosition);
        }
    }

    private ABC_Controller.Weapon FindWeaponEntity(WeaponState weaponState)
    {
        var weaponTypeName = weaponState.BaseParams.Title switch
        {
            var sword when sword.Contains("Меч") => "Sword",
            var knife when knife.Contains("Кинжал") => "Knife",
            var stave when stave.Contains("Посох") => "Stave",
            _ => ""
        };
        return _playerController.CurrentWeapons
            .Where(weapon => weapon.weaponName.Contains(weaponTypeName))
            .First();
    }

    private void UpdateWeaponGraphics((ABC_Controller.Weapon entity, GameObject physicalRepresentation) weapon)
    {
        weapon.entity.weaponGraphics[0].weaponObjMainGraphic.GameObject = weapon.physicalRepresentation;
        weapon.entity.weaponGraphics[0].CreateGraphicObject();
        weapon.entity.CreateObjectPools();
    }

    private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) GetWeaponByHandPosition(WeaponHandPosition weaponHandPosition)
    {
        return weaponHandPosition switch
        {
            WeaponHandPosition.Left => _leftWeapon,
            WeaponHandPosition.Right => _rightWeapon
        };
    }
}
