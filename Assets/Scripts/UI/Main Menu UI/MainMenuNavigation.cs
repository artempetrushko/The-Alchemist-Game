using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuNavigation : MonoBehaviour
{
    private MainMenuButton selectedButton;
    private int selectedButtonNumber;

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
                    selectedButtonNumber = GetComponentsInChildren<MainMenuButton>().ToList().IndexOf(selectedButton) + 1;
                }
            }
        }
    }
    private int SelectedButtonNumber
    {
        get => selectedButtonNumber;
        set
        {
            if (value != selectedButtonNumber && value > 0 && value <= transform.childCount)
            {
                SelectedButton = transform.GetChild(value - 1).GetComponent<MainMenuButton>();
            }
        }
    }

    public void StartNavigation()
    {
        SelectedButtonNumber = 1;
    }

    public void Navigate(InputAction.CallbackContext context) 
    { 
        var inputValueY = context.ReadValue<Vector2>().y;
        if (inputValueY == 1 || inputValueY == -1)        
        {
            SelectedButtonNumber -= (int)inputValueY;
        }
    }

    public void PressButton(InputAction.CallbackContext context) => SelectedButton.GetComponent<Button>().onClick.Invoke();

    private void Start()
    {
        StartNavigation();
    }
}
