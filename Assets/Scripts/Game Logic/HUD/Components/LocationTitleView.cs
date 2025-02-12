using Cysharp.Threading.Tasks;
using LeTai.TrueShadow;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class LocationTitleView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _locationTitleText;
        [SerializeField] private TrueShadow _titleShadow;

        public async UniTask ShowLocationTitleAsync(string locationTitle, float duration)
        {
            _titleShadow.enabled = true;
            _locationTitleText.text = "";

            var textAppearanceLatency = duration / locationTitle.Length;
            for (var i = 0; i < locationTitle.Length; i++)
            {
                _locationTitleText.text += locationTitle[i];
                await UniTask.WaitForSeconds(textAppearanceLatency);
            }
        }

        public async UniTask HideLocationTitleAsync(float duration)
        {
            var textHidingLatency = duration / _locationTitleText.text.Length;
            while (_locationTitleText.text.Length > 0)
            {
                _locationTitleText.text = _locationTitleText.text.Remove(_locationTitleText.text.Length - 1);
                await UniTask.WaitForSeconds(textHidingLatency);
            }

            _titleShadow.enabled = false;
        }
    }
}