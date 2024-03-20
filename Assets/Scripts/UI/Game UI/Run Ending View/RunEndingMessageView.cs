using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunEndingMessageView : MonoBehaviour
{
    [SerializeField]
    private Image messageIcon;
    [SerializeField]
    private TMP_Text messageText;
    [SerializeField]
    private CanvasGroup viewCanvasGroup;

    public void SetInfo(Sprite icon, string text)
    {
        messageIcon.sprite = icon;
        messageText.text = text;
    }

    public Tween GetShowMessageTween(float duration) => viewCanvasGroup.DOFade(1f, duration);

    public IEnumerator Show_COR(float duration)
    {
        var viewShowingTween = viewCanvasGroup.DOFade(1f, duration);
        yield return viewShowingTween.WaitForCompletion();
    }

    public void Hide() => viewCanvasGroup.alpha = 0f;
}
