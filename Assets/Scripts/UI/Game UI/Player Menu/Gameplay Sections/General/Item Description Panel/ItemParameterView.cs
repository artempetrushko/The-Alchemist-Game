using TMPro;
using UnityEngine;

namespace UI.PlayerMenu
{
    public class ItemParameterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _parameterTitleText;
        [SerializeField] private TMP_Text _parameterValueText;

        public void SetParameterTitleText(string text) => _parameterTitleText.text = text;

        public void SetParameterValueText(string text) => _parameterValueText.text = text;
    }
}