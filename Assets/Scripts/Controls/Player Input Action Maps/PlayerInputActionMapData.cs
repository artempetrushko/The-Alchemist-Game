using System;
using UnityEngine;

namespace Controls
{
    [Serializable]
    public class PlayerInputActionMapData
    {
        [SerializeField] private PlayerInputActionMap _actionMap;
        [SerializeField] private string _actionMapName;

        public PlayerInputActionMap ActionMap => _actionMap;
        public string ActionMapName => _actionMapName;
    }
}
