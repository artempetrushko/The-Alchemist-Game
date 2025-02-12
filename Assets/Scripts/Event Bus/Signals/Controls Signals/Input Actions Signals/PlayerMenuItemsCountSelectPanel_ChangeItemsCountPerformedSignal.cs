using UnityEngine.InputSystem;

namespace EventBus
{
    public class PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}