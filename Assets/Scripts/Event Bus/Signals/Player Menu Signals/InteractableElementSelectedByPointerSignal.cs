using GameLogic.PlayerMenu;

namespace EventBus
{
    public class InteractableElementSelectedByPointerSignal
    {
        public readonly IPlayerMenuInteractable InteractableElement;
        public readonly ISelectableCollection LinkedCollection;

        public InteractableElementSelectedByPointerSignal(IPlayerMenuInteractable interactableElement, ISelectableCollection linkedCollection)
        {
            InteractableElement = interactableElement;
            LinkedCollection = linkedCollection;
        }
    }
}