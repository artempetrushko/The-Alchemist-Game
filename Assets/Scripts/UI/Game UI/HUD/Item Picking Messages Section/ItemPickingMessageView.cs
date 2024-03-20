using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickingMessageView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemsCountText;
    [SerializeField]
    private Image itemIcon;

    public void SetInfo(Sprite itemIcon, int itemsCount)
    {
        this.itemIcon.sprite = itemIcon;
        itemsCountText.text = $"+{itemsCount}";
    }
}
