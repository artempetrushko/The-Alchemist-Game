using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestProgressView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup viewCanvasGroup;
    [SerializeField]
    private TMP_Text questDescriptionText;

    private bool isVisible;

    public void SetQuestDescription(string questDescription)
    {
        questDescriptionText.text = questDescription;
        isVisible = false;
        StartCoroutine(ShowQuestDescription_COR());
    }

    public IEnumerator ShowQuestDescription_COR()
    {
        if (!isVisible)
        {
            isVisible = true;
            var tweenSequence = DOTween.Sequence();
            tweenSequence.Append(viewCanvasGroup.DOFade(1f, 1f));
            tweenSequence.AppendInterval(5f);
            tweenSequence.Append(viewCanvasGroup.DOFade(0f, 1f));
            tweenSequence.Play();
            yield return tweenSequence.WaitForCompletion();
            isVisible = false;
        }       
    }
}
