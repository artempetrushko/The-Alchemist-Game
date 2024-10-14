using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuCraftSectionRecipesActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectRecipeActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
