using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuInventorySpecialInteractionActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _executeActionName;
        [SerializeField] private LocalizedString _cancelInteractionActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
