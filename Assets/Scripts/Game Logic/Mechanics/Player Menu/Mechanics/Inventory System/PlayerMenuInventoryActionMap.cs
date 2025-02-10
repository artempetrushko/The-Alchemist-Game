using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuInventoryActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _pressItemSlotActionName;
        [SerializeField] private LocalizedString _startItemMovingActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
