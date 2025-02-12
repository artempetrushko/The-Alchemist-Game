using UnityEngine.InputSystem;

namespace EventBus
{
    public class PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}