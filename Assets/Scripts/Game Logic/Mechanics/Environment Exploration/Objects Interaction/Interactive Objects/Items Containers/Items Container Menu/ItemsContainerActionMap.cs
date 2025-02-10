using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    [CreateAssetMenu(fileName = "Items Container Action Map", menuName = "Game Configs/Input/Action Maps/Items Container Action Map")]
    public class ItemsContainerActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _pickItemActionName;
        [SerializeField] private LocalizedString _pickAllItemsActionName;
        [SerializeField] private LocalizedString _closeContainerActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
