using Controls;
using System;
using UnityEngine;

namespace UI.PlayerMenu
{
    public class PlayerMenuSectionNavigation
    {
        public event Action<PlayerInputActionMap> CurrentSubsectionChanged;

        private PlayerMenuSubsectionNavigation defaultSubsection;

        public PlayerMenuSubsectionNavigation CurrentSubsection { get; private set; }

        public void SetCurrentSubsection(PlayerMenuSubsectionNavigation subsection, SubsectionNavigationStartCondition navigationStartCondition = SubsectionNavigationStartCondition.Default)
        {
            if (CurrentSubsection != null)
            {
                CurrentSubsection.StopNavigation();
            }
            CurrentSubsection = subsection;
            if (CurrentSubsection != null)
            {
                CurrentSubsectionChanged?.Invoke(CurrentSubsection.PlayerInputActionMap);
                CurrentSubsection.StartNavigation(navigationStartCondition);
            }
        }

        public void StartNavigation() => SetCurrentSubsection(defaultSubsection);

        public void NavigateCurrentSubsection(Vector2 inputValue) => CurrentSubsection.Navigate(inputValue);

        public void PressSelectedElement()
        {
            CurrentSubsection?.PressSelectedElement();
        }
    }
}