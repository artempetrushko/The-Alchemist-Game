using System;
using System.Collections.Generic;
using Controls;
using EventBus;
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

        public PlayerMenuPresenter(PlayerMenuView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;

            _signalBus.Subscribe<ShowPlayerMenuPerformedSignal>(ShowPlayerMenu);
            _signalBus.Subscribe<HidePlayerMenuPerformedSignal>(HidePlayerMenu);
            _signalBus.Subscribe<PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal>(OnNavigatePlayerMenuActionPerformed);
            _signalBus.Subscribe<PlayerMenuInventorySection_NavigateSectionPerformedSignal>(OnNavigateSectionActionPerformed); 
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShowPlayerMenuPerformedSignal>(ShowPlayerMenu);
            _signalBus.Unsubscribe<HidePlayerMenuPerformedSignal>(HidePlayerMenu);
            _signalBus.Unsubscribe<PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal>(OnNavigatePlayerMenuActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuInventorySection_NavigateSectionPerformedSignal>(OnNavigateSectionActionPerformed);
        }

        private void ShowPlayerMenu(ShowPlayerMenuPerformedSignal signal)
        {
            _signalBus.Fire<PlayerMenuEnabledSignal>();

            _view.SetActive(true);
            CreateSectionButtons();
            CurrentSectionButtonNumber = 1;
        }

        private void HidePlayerMenu(HidePlayerMenuPerformedSignal signal)
        {
            CurrentSectionButtonNumber = 0;

            _currentSection.Hide();
            _view.SetActive(false);

            _signalBus.Fire<PlayerMenuDisabledSignal>();
        }

        private void SetCurrentSelectableCollection(ISelectableCollection selectableCollection, IPlayerMenuInteractable selectedElement = null)
        {
            _currentSelectableCollection = selectableCollection;
            _signalBus.Fire(new ActionMapRequestedSignal(selectableCollection.InputActionMap));

            _selectedInteractableElement = selectedElement ?? _currentSelectableCollection.GetStartSelectedElement();
        }

        private void OnNavigatePlayerMenuActionPerformed(PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>().x;
            //CurrentSectionButtonNumber = Mathf.Clamp(CurrentSectionButtonNumber + (int)inputValue, 1, playerMenuNavigationSectionView.SectionButtonsCount);
        }

        private void OnNavigateSectionActionPerformed(PlayerMenuInventorySection_NavigateSectionPerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>();
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