 using Cysharp.Threading.Tasks;
using LeTai.TrueShadow;
using TMPro;
using UnityEngine;

namespace GameLogic.HUD
{
    public class LocationTitleView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text locationTitleText;
        [SerializeField]
        private TrueShadow titleShadow;
        [Space, SerializeField]
        private float titleAppearanceTime = 1f;

        public async UniTask ShowLocationNameAsync(string locationTitle)
        {
            locationTitleText.text = "";
            titleShadow.enabled = true;
            var textAppearanceLatency = titleAppearanceTime / locationTitle.Length;
            for (var i = 0; i < locationTitle.Length; i++)
            {
                locationTitleText.text += locationTitle[i];
                await UniTask.WaitForSeconds(textAppearanceLatency);
            }
            await UniTask.WaitForSeconds(5f);
            for (var i = 0; i < locationTitle.Length; i++)
            {
                locationTitleText.text = locationTitleText.text[..^1];
                await UniTask.WaitForSeconds(textAppearanceLatency / 2);
            }
            titleShadow.enabled = false;
        }
    }
}