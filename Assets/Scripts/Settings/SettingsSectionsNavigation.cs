using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsSectionsNavigation : MonoBehaviour
{
    private MainMenuButton selectedButton;
    private int selectedSettingsSectionNumber;

    public MainMenuButton SelectedButton
    {
        get => selectedButton;
        set
        {
            if (selectedButton != value)
            {
                if (selectedButton != null)
                {
                    selectedButton.ToggleState(false);
                }
                selectedButton = value;
                if (selectedButton != null)
                {
                    selectedButton.ToggleState(true);
                    selectedSettingsSectionNumber = GetComponentsInChildren<MainMenuButton>().ToList().IndexOf(selectedButton) + 1;
                }
            }
        }
    }
    private int SelectedSettingsSectionNumber
    {
        get => selectedSettingsSectionNumber;
        set
        {
            if (value != selectedSettingsSectionNumber && value > 0 && value <= transform.childCount)
            {
                SelectedButton = transform.GetChild(value - 1).GetComponent<MainMenuButton>();
            }
        }
    }

    public void StartNavigation()
    {
        SelectedSettingsSectionNumber = 2;
    }

    public void Navigate(InputAction.CallbackContext context)
    {
        var inputValueX = context.ReadValue<Vector2>().x;
        if (inputValueX == 1 || inputValueX == -1)
        {
            SelectedSettingsSectionNumber += (int)inputValueX;
        }
    }
}
