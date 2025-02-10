using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class RunEndingPanelView : MonoBehaviour
    {
        [SerializeField] private RunEndingMessageView _runEndingMessageView;
        [SerializeField] private CanvasGroup _actionButtonsContainer;
        [SerializeField] private ActionButton _returnToHubButton;
        [SerializeField] private ActionButton _exitToMainMenuButton;
        [SerializeField] private Image _background;

        public RunEndingMessageView RunEndingMessageView => _runEndingMessageView;
        public ActionButton ReturnToHubButton => _returnToHubButton;
        public ActionButton ExitToMainMenuButton => _exitToMainMenuButton;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public async UniTask ShowAsync()
        {
            await _background.DOFade(0.75f, 1f).AsyncWaitForCompletion();
            await UniTask.WaitForSeconds(0.75f);
            await _runEndingMessageView.PlayFadeAnimation(1f, 1f);
            await UniTask.WaitForSeconds(0.5f);
            await _actionButtonsContainer.DOFade(1f, 1f).AsyncWaitForCompletion();
        }
    }
}