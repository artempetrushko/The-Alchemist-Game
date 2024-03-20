using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuButtonData[] buttonDatas;
    [SerializeField]
    private GameObject buttonsContainer;
    [SerializeField] 
    private Button returnToMenuButton;
    [Space]
    [SerializeField]
    private GameObject settingsMenuSection;

    private GameObject currentOpenedMenuSection = null;

    public void ShowSettingsMenuSection()
    {
        //buttons.SetActive(false);
        settingsMenuSection.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        currentOpenedMenuSection = settingsMenuSection;
        //settingsMenuSection.GetComponentInChildren<GraphicsSettingsSection>().SendSettingsData();
    }

    public void ShowMenuSection(GameObject menuSection)
    {
        //buttons.SetActive(false);
        menuSection.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        currentOpenedMenuSection = menuSection;
    }

    public void ReturnToMainMenu()
    {
        currentOpenedMenuSection.SetActive(false);
        returnToMenuButton.gameObject.SetActive(false);
        //buttons.SetActive(true);
        GetComponentInChildren<MainMenuNavigation>().StartNavigation();
    }

    public void StartGame() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
