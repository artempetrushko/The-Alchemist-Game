using Controls;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public enum SubsectionNavigationStartCondition
    {
        Default,
        TransitionFromLeftSubsection,
        TransitionFromRightSubsection,
        TransitionFromTopSubsection,
        TransitionFromBottomSubsection
    }

    public abstract class PlayerMenuSubsectionNavigation : MonoBehaviour
    {
        [SerializeField]
        protected PlayerInputActionMap playerInputActionMap;
        [SerializeField]
        protected PlayerMenuSectionNavigation parentSection;
        [Space, SerializeField]
        protected PlayerMenuSubsectionNavigation leftNeighboringSubsection;
        [SerializeField]
        protected PlayerMenuSubsectionNavigation rightNeighboringSubsection;
        [SerializeField]
        protected PlayerMenuSubsectionNavigation topNeighboringSubsection;
        [SerializeField]
        protected PlayerMenuSubsectionNavigation bottomNeighboringSubsection;

        public PlayerInputActionMap PlayerInputActionMap => playerInputActionMap;
        public PlayerMenuSectionNavigation ParentSection
        {
            get => parentSection;
            set => parentSection = value;
        }

        public abstract void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default);

        public abstract void Navigate(Vector2 inputValue);

        public abstract void StopNavigation();

        public abstract void PressSelectedElement();
    }
}
