using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private Image arrowImage;

    private MainMenuNavigation mainMenuNavigation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenuNavigation.SelectedButton = this;
    }

    public void ToggleState(bool isSelected)
    {
        if (isSelected)
        {
            GetComponent<Button>().Select();
        }        
        arrowImage.transform.localScale = isSelected ? Vector3.one : Vector3.zero;
    }

    private void Start()
    {
        mainMenuNavigation = GetComponentInParent<MainMenuNavigation>();
    }
}
