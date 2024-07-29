using System;
using UnityEngine;

namespace UI.PlayerMenu
{
    public abstract class PlayerMenuSubsectionNavigation
    {
        public event Action LeftSubsectionSelected;
        public event Action RightSubsectionSelected;
        public event Action TopSubsectionSelected;
        public event Action BottomSubsectionSelected;

        public abstract void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default);

        public abstract void Navigate(Vector2 inputValue);

        public abstract void StopNavigation();

        public abstract void PressSelectedElement();
    }
}