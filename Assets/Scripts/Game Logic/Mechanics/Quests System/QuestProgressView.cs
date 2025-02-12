using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameLogic.QuestSystem
{
    public class QuestProgressView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _questDescriptionText;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetQuestDescriptionText(string text) => _questDescriptionText.text = text;

        public async UniTask PlayFadeAnimation(float endValue, float duration) => await _canvasGroup.DOFade(endValue, duration).AsyncWaitForCompletion();
    }
}