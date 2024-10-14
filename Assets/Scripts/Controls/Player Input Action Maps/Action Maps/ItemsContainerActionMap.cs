using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class ItemsContainerActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _takeItemActionName;
        [SerializeField] private LocalizedString _takeAllItemsActionName;
        [SerializeField] private LocalizedString _closeContainerActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
