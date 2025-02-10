using System.Collections;
using Controls;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLogic.RunEnding
{
    public class RunEndingView : MonoBehaviour
    {
        [SerializeField]
        private RunEndingMessageView runEndingMessageView;
        [Space, SerializeField]
        private ActionButton actionButtonPrefab;
        [SerializeField]
        private CanvasGroup actionButtonsContainer;
        [Space, SerializeField]
        private Image background;

        public void SetInfo(string message, Sprite statusIcon, (DetailedControlTip controlTip, UnityAction buttonPressedAction)[] actionButtonDatas)
        {
            runEndingMessageView.SetInfo(statusIcon, message);
            foreach (var actionButtonData in actionButtonDatas)
            {
                var actionButton = Instantiate(actionButtonPrefab, actionButtonsContainer.transform);
                actionButton.SetInfo(actionButtonData.controlTip, actionButtonData.buttonPressedAction);
            }
        }

        public IEnumerator Show_COR()
        {
            SetDefaultState();

            var tweenSequence = DOTween.Sequence();
            tweenSequence.Append(background.DOFade(0.75f, 1f));
            tweenSequence.AppendInterval(0.75f);
            tweenSequence.Append(runEndingMessageView.GetShowMessageTween(1f));
            tweenSequence.AppendInterval(0.5f);
            tweenSequence.Append(actionButtonsContainer.DOFade(1f, 1f));
            tweenSequence.Play();
            yield return tweenSequence.WaitForCompletion();
        }

        private void SetDefaultState()
        {
            background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
            actionButtonsContainer.alpha = 0f;
            runEndingMessageView.Hide();
        }
    }
}