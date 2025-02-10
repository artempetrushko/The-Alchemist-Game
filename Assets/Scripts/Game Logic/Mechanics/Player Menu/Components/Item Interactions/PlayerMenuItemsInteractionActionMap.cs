using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuItemsInteractionActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _executeActionName;
        [SerializeField] private LocalizedString _cancelActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
