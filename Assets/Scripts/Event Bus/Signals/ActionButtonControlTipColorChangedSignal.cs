using UnityEngine;

namespace EventBus
{
    public class ActionButtonControlTipColorChangedSignal
    {
        public readonly Color Color;

        public ActionButtonControlTipColorChangedSignal(Color color)
        {
            Color = color;
        }
    }
}