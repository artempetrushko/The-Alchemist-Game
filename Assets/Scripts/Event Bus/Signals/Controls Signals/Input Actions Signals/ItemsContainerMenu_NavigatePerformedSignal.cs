using UnityEngine.InputSystem;

namespace EventBus
{
    public class ItemsContainerMenu_NavigatePerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public ItemsContainerMenu_NavigatePerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}