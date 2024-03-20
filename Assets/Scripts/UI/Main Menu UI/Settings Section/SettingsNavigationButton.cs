using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsNavigationButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private Image underline;

    private SettingsSectionsNavigation settingsSectionsNavigation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void ToggleState(bool isSelected)
    {
        if (isSelected)
        {
            GetComponent<Button>().Select();
        }
        underline.gameObject.SetActive(isSelected);
    }
}
