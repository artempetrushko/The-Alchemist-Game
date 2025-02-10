using UnityEngine;

namespace Controls
{
    public class ControlTip
    {
        public string KeyName { get; private set; }
        public Sprite KeyIcon { get; private set; }

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