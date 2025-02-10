using Controls;

namespace GameLogic.PlayerMenu
{
    public interface ISelectableCollection
    {
        PlayerInputActionMap InputActionMap { get; }

        IPlayerMenuInteractable GetStartSelectedElement(PlayerMenuNavigationStartCondition startCondition = PlayerMenuNavigationStartCondition.Default);
    }
}