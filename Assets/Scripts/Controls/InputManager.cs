using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class InputManager
    {
        private PlayerInput _playerInput;
        private PlayerInputActionMapsConfig _actionMapsConfig;
        private GamepadButtonsIconsData _gamepadButtonsIcons;

        private ControlsTipsSectionView currentControlsTipsSectionView;
        private PlayerInputActionMap currentActionMap;
        private List<(string name, InputAction inputAction)> currentInputActions;

        public PlayerInputActions PlayerActions { get; private set; }
        public PlayerInputActionMap CurrentActionMap
        {
            get => currentActionMap;
            set
            {
                if (currentActionMap != value)
                {
                    currentActionMap = value;
                    var actionMapName = _actionMapsConfig.ActionMapDatas
                        .FirstOrDefault(actionMap => actionMap.ActionMap == currentActionMap).ActionMapName;
                    _playerInput.SwitchCurrentActionMap(actionMapName);
                    if (actionMapName.Contains("Player Menu"))
                    {
                        
                    }
                }
            }
        }
        public ControlsTipsSectionView CurrentControlsTipsSectionView
        {
            get => currentControlsTipsSectionView;
            set
            {
                if (currentControlsTipsSectionView != value)
                {
                    currentControlsTipsSectionView = value;

                }
            }
        }

        public InputManager(PlayerInput playerInput, PlayerInputActionMapsConfig actionMapsConfig, GamepadButtonsIconsData gamepadButtonsIconsData)
        {
            _playerInput = playerInput;
            _actionMapsConfig = actionMapsConfig;
            _gamepadButtonsIcons = gamepadButtonsIconsData;
        }

        public void Initialize()
        {
            PlayerActions = new();
            CurrentActionMap = _actionMapsConfig.ActionMapDatas
                .FirstOrDefault(actionMap => actionMap.ActionMapName == _playerInput.defaultActionMap).ActionMap;
        }

        public void ShowCurrentControlsTips(ControlsTipsSectionView controlsTipsSectionView, (string name, InputAction inputAction)[] actionDatas)
        {
            var detailedControlsTips = CreateDetailedControlsTips(actionDatas);
            controlsTipsSectionView.SetContent(detailedControlsTips);
        }

        public DetailedControlTip CreateDetailedControlsTip((string name, InputAction inputAction) action)
        {
            return _playerInput.currentControlScheme switch
            {
                "Gamepad" => new DetailedControlTip(action.name, GetGamepadButtonIcon(action.inputAction)),
                "KeyboardMouse" => new DetailedControlTip(action.name, action.inputAction.controls[0].displayName)
            };
        }

        public DetailedControlTip[] CreateDetailedControlsTips((string name, InputAction inputAction)[] actionDatas)
        {
            return actionDatas
                .Select(actionData => CreateDetailedControlsTip(actionData))
                .ToArray();
        }

        public void SubscribeControlsChangedEvent(Action listener)
        {
            _playerInput.controlsChangedEvent.AddListener((playerInput) => listener());
        }

        public void UnsubscribeControlsChangedEvent(Action listener)
        {
            _playerInput.controlsChangedEvent.RemoveListener((playerInput) => listener());
        }

        private Sprite GetGamepadButtonIcon(InputAction inputAction)
        {
            var actionControl = inputAction.controls.Where(x => x.device.name.Contains("Gamepad")).First();
            return actionControl.displayName switch
            {
                "Button North" or "Triangle" => _gamepadButtonsIcons.NorthButtonIcon,
                "Button South" or "Cross" => _gamepadButtonsIcons.SouthButtonIcon,
                "Button East" or "Circle" => _gamepadButtonsIcons.EastButtonIcon,
                "Button West" or "Square" => _gamepadButtonsIcons.WestButtonIcon,
                "D-Pad Up" => _gamepadButtonsIcons.DPadUpButtonIcon,
                "D-Pad Down" => _gamepadButtonsIcons.DPadDownButtonIcon,
                "D-Pad Left" => _gamepadButtonsIcons.DPadLeftButtonIcon,
                "D-Pad Right" => _gamepadButtonsIcons.DPadRightButtonIcon
            };
        }

        private List<(string name, InputAction inputAction)> GetPlayerAvailableActions(PlayerInputActionMap actionMap)
        {
            return actionMap switch
            {
                PlayerInputActionMap.PlayerMenu_Inventory => new()
            {
                ("Взаимодействовать", PlayerActions.PlayerMenuInventorySection.PressItemCell),
                ("Переместить предмет", PlayerActions.PlayerMenuInventorySection.StartItemMoving)
            },
                PlayerInputActionMap.PlayerMenu_Inventory_SpecialInteraction => new()
            {
                ("Отменить", PlayerActions.PlayerMenuInventorySectionSpecialInteraction.CancelInteraction),
                ("Выбрать", PlayerActions.PlayerMenuInventorySectionSpecialInteraction.Execute)
            },
                PlayerInputActionMap.PlayerMenu_ItemCellActionsMenu => new()
            {
                ("Назад", PlayerActions.PlayerMenuItemCellActionsMenu.CloseItemCellActionsMenu),
                ("Выбрать", PlayerActions.PlayerMenuItemCellActionsMenu.Select)
            },
                PlayerInputActionMap.PlayerMenu_InventoryItemMoving => new()
            {
                ("Положить", PlayerActions.PlayerMenuInventoryItemMoving.PutItemDown)
            },
                PlayerInputActionMap.PlayerMenu_CraftSection_Recipes => new()
            {
                ("Выбрать", PlayerActions.PlayerMenuCraftSectionRecipes.Select)
            },
                PlayerInputActionMap.PlayerMenu_CraftSection_EnergyCells => new()
            {
                ("Выбрать", PlayerActions.PlayerMenuCraftSectionEnergyCells.Select),
                ("Перейти к ингредиентам", PlayerActions.PlayerMenuCraftSectionEnergyCells.GoToCraftingItemTemplate),
                ("(удерж.) Изготовить", PlayerActions.PlayerMenuCraftSectionEnergyCells.CreateItem)
            },
                PlayerInputActionMap.PlayerMenu_CraftSection_CraftingItemTemplate => new()
            {
                ("Назад", PlayerActions.PlayerMenuCraftSectionCraftingItemTemplate.ReturnToEnergyCells),
                ("Очистить", PlayerActions.PlayerMenuCraftSectionCraftingItemTemplate.ReturnItemToInventory),
                ("Выбрать", PlayerActions.PlayerMenuCraftSectionCraftingItemTemplate.Select),
                ("(удерж.) Изготовить", PlayerActions.PlayerMenuCraftSectionCraftingItemTemplate.CreateItem)
            },
                PlayerInputActionMap.PlayerMenu_CraftSection_Inventory => new()
            {
                ("Выбрать", PlayerActions.PlayerMenuCraftSectionInventory.Select)
            },
            };
        }
    }
}