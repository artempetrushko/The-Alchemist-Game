using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMenuNavigationSectionView : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuSectionButton sectionButtonPrefab;
    [SerializeField]
    private GameObject sectionButtonsContainer;
    [Space, SerializeField]
    private ControlTipView leftSwitchSectionTipView;
    [SerializeField]
    private ControlTipView rightSwitchSectionTipView;

    public int SectionButtonsCount => sectionButtonsContainer.transform.childCount;

    public void CreateSectionButtons(List<(Sprite sectionIcon, UnityAction buttonAction)> buttonDatas)
    {
        ClearSectionButtons();
        foreach (var buttonData in buttonDatas)
        {
            var sectionButton = Instantiate(sectionButtonPrefab, sectionButtonsContainer.transform);
            sectionButton.SetInfo(buttonData.sectionIcon, buttonData.buttonAction);
        }
    }

    public void SetSectionButtonState(int buttonNumber, bool isSelected) => sectionButtonsContainer.transform.GetChild(buttonNumber - 1).GetComponent<PlayerMenuSectionButton>().SetButtonState(isSelected);

    public void UpdateTipViews((ControlTip switchLeftSectionTip, ControlTip switchRightSectionTip) switchSectionTips)
    {
        leftSwitchSectionTipView.SetInfo(switchSectionTips.switchLeftSectionTip);
        rightSwitchSectionTipView.SetInfo(switchSectionTips.switchRightSectionTip);
    }

    private void ClearSectionButtons()
    {
        for (var i = sectionButtonsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(sectionButtonsContainer.transform.GetChild(i).gameObject);
        }
    }
}
