using UnityEngine.InputSystem;

namespace EventBus
{
    public class Player_SelectQuickAccessCellPerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public Player_SelectQuickAccessCellPerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}