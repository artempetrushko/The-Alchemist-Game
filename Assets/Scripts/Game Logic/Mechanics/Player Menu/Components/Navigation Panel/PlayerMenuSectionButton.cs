using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMenuSectionButton : MonoBehaviour
{
    [SerializeField]
    private Button defaultStateButton;
    [SerializeField]
    private Image defaultStateButtonIcon;
    [SerializeField]
    private Image selectedStateButton;
    [SerializeField]
    private Image selectedStateButtonIcon;

    public void SetInfo(Sprite sectionIcon, UnityAction buttonAction)
    {
        defaultStateButtonIcon.sprite = selectedStateButtonIcon.sprite = sectionIcon;
        defaultStateButton.onClick.AddListener(buttonAction);
    }

    public void SetButtonState(bool isSelected)
    {
        defaultStateButton.gameObject.SetActive(!isSelected);
        selectedStateButton.gameObject.SetActive(isSelected);
    }
}
