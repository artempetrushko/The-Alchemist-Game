using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    private Sprite itemSprite;
    private Sprite itemBigSprite;

    public ItemState LinkedItem { get; set; }

    public void SetInfo(Sprite itemIcon, Sprite itemBigIcon = null)
    {
        itemSprite = itemIcon;
        itemBigSprite = itemBigIcon;
        this.itemIcon.sprite = itemSprite;
    }

    public void AdjustViewSize(Vector2 viewContainerSize)
    {
        if (viewContainerSize.x != viewContainerSize.y)
        {
            if (itemBigSprite != null)
            {
                itemIcon.sprite = itemBigSprite;
                itemIcon.rectTransform.sizeDelta = viewContainerSize;
            }
            else
            {
                var viewContainerMinSize = Mathf.Min(viewContainerSize.x, viewContainerSize.y);
                itemIcon.rectTransform.sizeDelta = new Vector2(viewContainerMinSize, viewContainerMinSize);
            }
        }
        else
        {
            itemIcon.rectTransform.sizeDelta = viewContainerSize;
        }
    }
}
