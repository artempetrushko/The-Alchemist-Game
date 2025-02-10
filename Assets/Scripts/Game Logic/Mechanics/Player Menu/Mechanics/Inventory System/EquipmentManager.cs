using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.PlayerMenu
{
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField]
        private ABC_Controller playerController;

        private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) leftWeapon;
        private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) rightWeapon;
        private (ABC_Controller.Weapon entity, GameObject physicalRepresentation) currentWeapon;

        public void SelectLeftHandWeapon(InputAction.CallbackContext context) => SelectWeapon(context, WeaponHandPosition.Left);

        public void SelectRightHandWeapon(InputAction.CallbackContext context) => SelectWeapon(context, WeaponHandPosition.Right);

        public void SelectEquippedWeapon(WeaponHandPosition weaponHandPosition)
        {
            if (currentWeapon != (null, null))
            {
                playerController.DisableWeapon(currentWeapon.entity.weaponID);
            }
            currentWeapon = GetWeaponByHandPosition(weaponHandPosition);
            if (currentWeapon != (null, null))
            {
                playerController.EnableWeapon(currentWeapon.entity.weaponID);
                UpdateWeaponGraphics(currentWeapon);
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
                if (currentWeapon == weapon)
                {
                    playerController.EnableWeapon(weapon.entity.weaponID);
                    UpdateWeaponGraphics(weapon);
                }
            }
            else
            {
                playerController.DisableWeapon(weapon.entity.weaponID);
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
            return playerController.CurrentWeapons
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
                WeaponHandPosition.Left => leftWeapon,
                WeaponHandPosition.Right => rightWeapon
            };
        }
    }
}