using UnityEngine.InputSystem;

namespace EventBus
{
    public class PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}