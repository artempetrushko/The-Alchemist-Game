using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public abstract class PlayerInputActionMap : ScriptableObject
    {
        [SerializeField] private string _name;

        public string Name => _name;

        public abstract (string name, InputAction inputAction)[] GetActionInfos();
    }
}
