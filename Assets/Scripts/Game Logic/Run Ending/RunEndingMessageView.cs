using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class RunEndingMessageView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _messageIcon;
        [SerializeField] private TMP_Text _messageText;

        public void SetMessageIcon(Sprite icon) => _messageIcon.sprite = icon;

        public void SetMessageText(string text) => _messageText.text = text;

        public async UniTask PlayFadeAnimation(float endValue, float duration) => await _canvasGroup.DOFade(endValue, duration).AsyncWaitForCompletion();
    }
}