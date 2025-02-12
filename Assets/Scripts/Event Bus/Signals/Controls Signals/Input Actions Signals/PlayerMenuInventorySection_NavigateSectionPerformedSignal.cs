using UnityEngine.InputSystem;

namespace EventBus
{
    public class PlayerMenuInventorySection_NavigateSectionPerformedSignal
    {
        public readonly InputAction.CallbackContext Context;

        public PlayerMenuInventorySection_NavigateSectionPerformedSignal(InputAction.CallbackContext context)
        {
            Context = context;
        }
    }
}