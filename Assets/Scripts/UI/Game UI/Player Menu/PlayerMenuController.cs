using Controls;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.PlayerMenu
{
    public class PlayerMenuController : IDisposable
    {
        [SerializeField]
        private GameObject playerMenuView;
        [SerializeField]
        private PlayerMenuNavigationSectionView playerMenuNavigationSectionView;
        //private PlayerMenuMechanicsData[] playerMenuMechanicsDatas;
        [SerializeField]
        private GameObject playerMenuSectionsContainer;
        private InputManager playerInputManager;

        private PlayerMenuSectionView currentSectionView;
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
                        playerMenuNavigationSectionView.SetSectionButtonState(currentSectionButtonNumber, false);
                        CurrentSectionView = null;
                    }
                    currentSectionButtonNumber = value;
                    if (currentSectionButtonNumber != 0)
                    {
                        playerMenuNavigationSectionView.SetSectionButtonState(currentSectionButtonNumber, true);
                        //EnablePlayerMenuMechanics(playerMenuMechanicsDatas[currentSectionButtonNumber - 1]);
                    }
                }
            }
        }
        public PlayerMenuSectionView CurrentSectionView
        {
            get => currentSectionView;
            private set
            {
                if (currentSectionView != value)
                {
                    if (currentSectionView != null)
                    {
                        currentSectionView.SectionNavigation.CurrentSubsectionChanged -= SetCurrentSubsectionActionMap;
                        //Destroy(currentSectionView.gameObject);
                    }
                    currentSectionView = value;
                    if (currentSectionView != null)
                    {
                        currentSectionView.SectionNavigation.CurrentSubsectionChanged += SetCurrentSubsectionActionMap;
                        currentSectionView.SectionNavigation.StartNavigation();
                    }
                }
            }
        }

        public PlayerMenuController()
        {
            playerInputManager.PlayerActions.Player.ShowPlayerMenu.performed += ShowPlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed += HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuAlchemistrySectionRecipes.ClosePlayerMenu.performed += HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionEnergyCells.ClosePlayerMenu.performed += HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionInventory.ClosePlayerMenu.performed += HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionRecipes.ClosePlayerMenu.performed += HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuSettings.ClosePlayerMenu.performed += HidePlayerMenu;

            playerInputManager.PlayerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed += ChangePlayerMenuSection;
            playerInputManager.PlayerActions.PlayerMenuInventorySection.NavigateSection.performed += NavigateCurrentSubsection;
        }

        public void Dispose()
        {
            playerInputManager.PlayerActions.Player.ShowPlayerMenu.performed -= ShowPlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed -= HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuAlchemistrySectionRecipes.ClosePlayerMenu.performed -= HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionEnergyCells.ClosePlayerMenu.performed -= HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionInventory.ClosePlayerMenu.performed -= HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuCraftSectionRecipes.ClosePlayerMenu.performed -= HidePlayerMenu;
            playerInputManager.PlayerActions.PlayerMenuSettings.ClosePlayerMenu.performed -= HidePlayerMenu;

            playerInputManager.PlayerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed -= ChangePlayerMenuSection;
        }

        private void ShowPlayerMenu(InputAction.CallbackContext context)
        {
            playerMenuView.SetActive(true);
            CreateSectionButtons();
            Time.timeScale = 0;
            CurrentSectionButtonNumber = 1;
        }

        private void HidePlayerMenu(InputAction.CallbackContext context)
        {
            CurrentSectionButtonNumber = 0;
            playerMenuView.SetActive(false);
            playerInputManager.CurrentActionMap = PlayerInputActionMap.Player;
            Time.timeScale = 1;
        }

        private void ChangePlayerMenuSection(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>().x;
            CurrentSectionButtonNumber = Mathf.Clamp(CurrentSectionButtonNumber + (int)inputValue, 1, playerMenuNavigationSectionView.SectionButtonsCount);
        }

        private void NavigateCurrentSubsection(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            CurrentSectionView.SectionNavigation.NavigateCurrentSubsection(inputValue);
        }

        private void PressSelectedElement(InputAction.CallbackContext context)
        {
            if (CurrentSectionView != null)
            {
                CurrentSectionView.SectionNavigation.PressSelectedElement();
            }
        }

        private void SetCurrentSubsectionActionMap(PlayerInputActionMap actionMap) => playerInputManager.CurrentActionMap = actionMap;

        private void CreateSectionButtons()
        {
            var sectionButtonDatas = new List<(Sprite sectionIcon, UnityAction buttonAction)>();
            /*for (var i = 1; i <= playerMenuMechanicsDatas.Length; i++)
            {
                var sectionNumber = i;
                sectionButtonDatas.Add((playerMenuMechanicsDatas[i - 1].SectionIcon, () => CurrentSectionButtonNumber = sectionNumber));
            }*/
            playerMenuNavigationSectionView.CreateSectionButtons(sectionButtonDatas);
        }

        /*private void EnablePlayerMenuMechanics(PlayerMenuMechanicsData mechanicsData)
        {
            var linkedMechanicsView = Instantiate(mechanicsData.MechanicsViewPrefab, playerMenuSectionsContainer.transform);
            mechanicsData.MechanicsManager.InitializeLinkedView(linkedMechanicsView);
            CurrentSectionView = linkedMechanicsView;
        }*/
    }
}