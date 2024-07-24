using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public async UniTask ShowAsync()
    {
        SetDefaultState();

        var tweenSequence = DOTween.Sequence();
        tweenSequence
            .Append(background.DOFade(0.75f, 1f))
            .AppendInterval(0.75f)
            .Append(runEndingMessageView.GetShowMessageTween(1f))
            .AppendInterval(0.5f)
            .Append(actionButtonsContainer.DOFade(1f, 1f));
        tweenSequence.Play();
        await tweenSequence.AsyncWaitForCompletion();
    }

    private void SetDefaultState()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
        actionButtonsContainer.alpha = 0f;
        runEndingMessageView.Hide();
    }
}
