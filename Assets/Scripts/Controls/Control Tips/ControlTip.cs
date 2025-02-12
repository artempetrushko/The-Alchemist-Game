using UnityEngine;

namespace Controls
{
    public class ControlTip
    {
        public readonly string KeyName;
        public readonly Sprite KeyIcon;

        public ControlTip(string keyName)
        {
            KeyName = keyName;
        }

        public ControlTip(Sprite keyIcon)
        {
            KeyIcon = keyIcon;
        }
    }
}