using TMPro;
using UnityEngine;

namespace Controls
{
    public class DetailedControlTipView : ControlTipView
    {
        [SerializeField] private TMP_Text _actionTitleText;

        public void SetActionTitleText(string text) => _actionTitleText.text = text;

        public void SetActionTitleTextColor(Color color) => _actionTitleText.color = color;
    }
}