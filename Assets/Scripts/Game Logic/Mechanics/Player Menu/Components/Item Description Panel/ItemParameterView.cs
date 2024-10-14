using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemParameterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _parameterTitleText;
        [SerializeField] private TMP_Text _parameterValueText;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetParameterTitleText(string text) => _parameterTitleText.text = text;

        public void SetParameterValueText(string text) => _parameterValueText.text = text;
    }
}