using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Button buttonComponent;
    [SerializeField]
    private DetailedControlTipView tipView;
    [SerializeField]
    private Color normalStateContentColor;
    [SerializeField]
    private Color selectedStateContentColor;    

    public void SetInfo(DetailedControlTip detailedControlTip, UnityAction buttonPressedAction)
    {
        buttonComponent.onClick.AddListener(buttonPressedAction);
        tipView.SetInfo(detailedControlTip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tipView.ChangeContentColor(selectedStateContentColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tipView.ChangeContentColor(normalStateContentColor);
    }

    private void OnEnable()
    {
        tipView.ChangeContentColor(normalStateContentColor);
    }
}
