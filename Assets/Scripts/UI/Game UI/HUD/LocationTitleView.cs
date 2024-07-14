using LeTai.TrueShadow;
using TMPro;
using UnityEngine;

namespace UI.Hud
{
    public class LocationTitleView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _locationTitleText;
        [SerializeField] private TrueShadow _titleShadow;

        public void SetLocationTitleText(string text)
        {
            _locationTitleText.text = text;
        }

        public void AddLocationTitleSymbol(char titleSymbol)
        {
            _locationTitleText.text += titleSymbol;
        }

        public void RemoveLocationTitleLastSymbol()
        {
            _locationTitleText.text = _locationTitleText.text[..^1];
        }

        public void SetTitleShadowEnable(bool isEnable)
        {
            _titleShadow.enabled = isEnable;
        }
    }
}