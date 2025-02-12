using UnityEngine.InputSystem;

namespace EventBus
{
    public class Player_SelectNeighboringQuickAccessCellPerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public Player_SelectNeighboringQuickAccessCellPerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}