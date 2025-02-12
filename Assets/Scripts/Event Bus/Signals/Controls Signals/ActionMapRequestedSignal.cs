using Controls;

namespace EventBus
{
    public class ActionMapRequestedSignal
    {
        public readonly PlayerInputActionMap ActionMap;

        public ActionMapRequestedSignal(PlayerInputActionMap actionMap)
        {
            ActionMap = actionMap;
        }
    }
}