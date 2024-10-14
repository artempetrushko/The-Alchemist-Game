using System;
using System.Collections.Generic;
using Controls;
using GameLogic.PlayerMenu.Craft;
using GameLogic.PlayerMenu.Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class PlayerMenuPresenter : IDisposable
    {
        private PlayerMenuConfig _config;
        private PlayerMenuView _view;
        private InventoryPresenter _mainInventoryPresenter;
        private CraftPresenter _craftPresenter;
        private InputManager _inputManager;
        private SignalBus _signalBus;

        private (IPlayerMenuSectionPresenter section, PlayerMenuSectionSelectButton linkedButton)[] _sectionInfos;
        private IPlayerMenuSectionPresenter _currentSection;
        private ISelectableCollection _currentSelectableCollection;
        private int currentSectionButtonNumber;

        public int CurrentSectionButtonNumber
        {
            get => currentSectionButtonNumber;
            private set
            {
                if (currentSectionButtonNumber != value)
                {
                    if (currentSectionButtonNumber != 0)
                    {
                        //playerMenuNavigationSectionView.SetSectionButtonState(currentSectionButtonNumber, false);
                        //CurrentSectionView = null;
                    }
                    currentSectionButtonNumber = value;
                    if (currentSectionButtonNumber != 0)
                    {
                        //playerMenuNavigationSectionView.SetSectionButtonState(currentSectionButtonNumber, true);
                        //EnablePlayerMenuMechanics(playerMenuMechanicsDatas[currentSectionButtonNumber - 1]);
                    }
                }
            }
        }

        private IPlayerMenuInteractable _selectedInteractableElement;

        public IPlayerMenuInteractable SelectedInteractableElement
        {
            get => _selectedInteractableElement;
            private set
            {
                if (_selectedInteractableElement != value)
                {
                    _selectedInteractableElement?.Deselect();
                    _selectedInteractableElement = value;
                    _selectedInteractableElement?.Select();

                    if (_selectedInteractableElement is ItemSlot itemSlot)
                    {
                        _signalBus.Fire(new SelectedItemSlotChangedSignal(itemSlot));
                    }
                }
            }
        }

        public PlayerMenuPresenter(PlayerMenuView view)
        {
            _view = view;

            _inputManager.PlayerActions.Player.ShowPlayerMenu.performed += ShowPlayerMenu;
            _inputManager.PlayerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed += OnNavigatePlayerMenuActionPerformed;
            _inputManager.PlayerActions.PlayerMenuInventorySection.NavigateSection.performed += OnNavigateSectionActionPerformed;
            _inputManager.PlayerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed += HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuAlchemistrySectionRecipes.ClosePlayerMenu.performed += HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionEnergyCells.ClosePlayerMenu.performed += HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionInventory.ClosePlayerMenu.performed += HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionRecipes.ClosePlayerMenu.performed += HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuSettings.ClosePlayerMenu.performed += HidePlayerMenu;           
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.Player.ShowPlayerMenu.performed -= ShowPlayerMenu;
            _inputManager.PlayerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed -= OnNavigatePlayerMenuActionPerformed;
            _inputManager.PlayerActions.PlayerMenuInventorySection.NavigateSection.performed -= OnNavigateSectionActionPerformed;
            _inputManager.PlayerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed -= HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuAlchemistrySectionRecipes.ClosePlayerMenu.performed -= HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionEnergyCells.ClosePlayerMenu.performed -= HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionInventory.ClosePlayerMenu.performed -= HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuCraftSectionRecipes.ClosePlayerMenu.performed -= HidePlayerMenu;
            _inputManager.PlayerActions.PlayerMenuSettings.ClosePlayerMenu.performed -= HidePlayerMenu;       
        }

        private void ShowPlayerMenu(InputAction.CallbackContext context)
        {
            _signalBus.Fire<PlayerMenuEnabledSignal>();

            _view.SetActive(true);
            CreateSectionButtons();
            CurrentSectionButtonNumber = 1;
        }

        private void HidePlayerMenu(InputAction.CallbackContext context)
        {
            CurrentSectionButtonNumber = 0;

            _currentSection.Hide();
            _view.SetActive(false);

            _signalBus.Fire<PlayerMenuDisabledSignal>();
        }

        private void SetCurrentSelectableCollection(ISelectableCollection selectableCollection, IPlayerMenuInteractable selectedElement = null)
        {
            _currentSelectableCollection = selectableCollection;
            _inputManager.SetActionMap(selectableCollection.InputActionMap);

            _selectedInteractableElement = selectedElement ?? _currentSelectableCollection.GetStartSelectedElement();
        }

        private void OnNavigatePlayerMenuActionPerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>().x;
            //CurrentSectionButtonNumber = Mathf.Clamp(CurrentSectionButtonNumber + (int)inputValue, 1, playerMenuNavigationSectionView.SectionButtonsCount);
        }

        private void OnNavigateSectionActionPerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            if (Mathf.Abs(inputValue.x) == 1)
            {
                SelectedInteractableElement = inputValue.x switch
                {
                    1 => SelectedInteractableElement.Neighbours.Right,
                    -1 => SelectedInteractableElement.Neighbours.Left,
                };
            }
            if (Mathf.Abs(inputValue.y) == 1)
            {
                SelectedInteractableElement = inputValue.y switch
                {
                    1 => SelectedInteractableElement.Neighbours.Top,
                    -1 => SelectedInteractableElement.Neighbours.Bottom
                };
            }
        }

        private void OnPressSelectedElementActionPerformed(InputAction.CallbackContext context) => SelectedInteractableElement.Click();

        private void CreateSectionButtons()
        {
            var sectionButtonDatas = new List<(Sprite sectionIcon, UnityAction buttonAction)>();
            /*for (var i = 1; i <= playerMenuMechanicsDatas.Length; i++)
            {
                var sectionNumber = i;
                sectionButtonDatas.Add((playerMenuMechanicsDatas[i - 1].SectionIcon, () => CurrentSectionButtonNumber = sectionNumber));
            }*/
            
        }

        /*public void UpdateControlTipViews(DetailedControlTip[] controlTips)
        {
            _controlsTipsSectionView.DetailedControlTipViewsContainer.transform.DeleteAllChildren();

            foreach (var controlTip in controlTips)
            {
                var controlTipView = Object.Instantiate(_detailedControlTipViewPrefab, _controlsTipsSectionView.DetailedControlTipViewsContainer.transform);
                controlTipView.SetInfo(controlTip);
            }
        }*/

        private void OnInteractableElementSelectedByPointer(InteractableElementSelectedByPointerSignal signal) => SetCurrentSelectableCollection(signal.LinkedCollection, signal.InteractableElement);
    }
}