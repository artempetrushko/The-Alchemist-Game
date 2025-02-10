using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public enum PlayerInputActionMap
    {
        None,
        Player,
        HUD_ItemsContainer,
        UI_RunEndingScreen,
        PlayerMenu_Inventory,
        PlayerMenu_Inventory_SpecialInteraction,
        PlayerMenu_ItemCellActionsMenu,
        PlayerMenu_InventoryItemMoving,
        PlayerMenu_ItemsCountChoiceView,
        PlayerMenu_CraftSection_Recipes,
        PlayerMenu_CraftSection_EnergyCells,
        PlayerMenu_CraftSection_CraftingItemTemplate,
        PlayerMenu_CraftSection_Inventory,
        PlayerMenu_AlchemistrySection_Recipes,
        PlayerMenu_Settings,
        UI_LoadingScreen
    }

    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;
        [SerializeField]
        private GamepadButtonsIcons gamepadButtonsIcons;

        private ControlsTipsSectionView currentControlsTipsSectionView;
        private PlayerInputActionMap currentActionMap;
        private List<(string name, InputAction inputAction)> currentInputActions;
        private Dictionary<PlayerInputActionMap, string> actionMapNames = new()
    {
        { PlayerInputActionMap.Player, "Player" },
        { PlayerInputActionMap.HUD_ItemsContainer,"HUD (Items Container)" },
        { PlayerInputActionMap.UI_LoadingScreen , "Loading Screen" },
        { PlayerInputActionMap.UI_RunEndingScreen,"Run Ending Screen" },
        { PlayerInputActionMap.PlayerMenu_Inventory, "Player Menu (Inventory Section)" },
        { PlayerInputActionMap.PlayerMenu_Inventory_SpecialInteraction, "Player Menu (Inventory (Special Interaction))" },
        { PlayerInputActionMap.PlayerMenu_ItemCellActionsMenu, "Player Menu (Item Cell Actions Menu)" },
        { PlayerInputActionMap.PlayerMenu_InventoryItemMoving, "Player Menu (Inventory Item Moving)" },
        { PlayerInputActionMap.PlayerMenu_ItemsCountChoiceView, "Player Menu (Choose Items Count Panel)" },
        { PlayerInputActionMap.PlayerMenu_CraftSection_Recipes, "Player Menu (Craft Section (Recipes))" },
        { PlayerInputActionMap.PlayerMenu_CraftSection_EnergyCells, "Player Menu (Craft Section (Energy Cells))" },
        { PlayerInputActionMap.PlayerMenu_CraftSection_CraftingItemTemplate, "Player Menu (Craft Section (Crafting Item Template))" },
        { PlayerInputActionMap.PlayerMenu_CraftSection_Inventory, "Player Menu (Craft Section (Inventory))" },
        { PlayerInputActionMap.PlayerMenu_AlchemistrySection_Recipes, "Player Menu (Alchemistry Section (Recipes))" },
        { PlayerInputActionMap.PlayerMenu_Settings, "Player Menu (Settings Section)" },
    };

        public PlayerInputActions PlayerActions { get; private set; }
        public PlayerInputActionMap CurrentActionMap
        {
            get => currentActionMap;
            set
            {
                if (currentActionMap != value)
                {
                    currentActionMap = value;
                    var actionMapName = actionMapNames[currentActionMap];
                    playerInput.SwitchCurrentActionMap(actionMapName);
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

        public void Initialize()
        {
            PlayerActions = new();
            CurrentActionMap = actionMapNames.FirstOrDefault(pair => pair.Value == playerInput.defaultActionMap).Key;
        }

        public void ShowCurrentControlsTips(ControlsTipsSectionView controlsTipsSectionView, (string name, InputAction inputAction)[] actionDatas)
        {
            var detailedControlsTips = CreateDetailedControlsTips(actionDatas);
            controlsTipsSectionView.SetContent(detailedControlsTips);
        }

        public DetailedControlTip CreateDetailedControlsTip((string name, InputAction inputAction) action)
        {
            return playerInput.currentControlScheme switch
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
            playerInput.controlsChangedEvent.AddListener((playerInput) => listener());
        }

        public void UnsubscribeControlsChangedEvent(Action listener)
        {
            playerInput.controlsChangedEvent.RemoveListener((playerInput) => listener());
        }

        private Sprite GetGamepadButtonIcon(InputAction inputAction)
        {
            var actionControl = inputAction.controls.Where(x => x.device.name.Contains("Gamepad")).First();
            return actionControl.displayName switch
            {
                "Button North" or "Triangle" => gamepadButtonsIcons.NorthButtonIcon,
                "Button South" or "Cross" => gamepadButtonsIcons.SouthButtonIcon,
                "Button East" or "Circle" => gamepadButtonsIcons.EastButtonIcon,
                "Button West" or "Square" => gamepadButtonsIcons.WestButtonIcon,
                "D-Pad Up" => gamepadButtonsIcons.DPadUpButtonIcon,
                "D-Pad Down" => gamepadButtonsIcons.DPadDownButtonIcon,
                "D-Pad Left" => gamepadButtonsIcons.DPadLeftButtonIcon,
                "D-Pad Right" => gamepadButtonsIcons.DPadRightButtonIcon
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