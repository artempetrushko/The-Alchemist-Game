using TMPro;
using UnityEngine;

public class ItemDescriptionPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text itemTitleText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject parametersSection;
    [SerializeField] private ItemParameterView parameterViewPrefab;
    [Space]
    [SerializeField] private CanvasGroup viewCanvasGroup;

    public void Show() => viewCanvasGroup.alpha = 1f;

    public void Hide() => viewCanvasGroup.alpha = 0f;

    public void SetItemTitleText(string text) => itemDescriptionText.text = text;

    public void SetItemDescriptionText(string text) => itemDescriptionText.text = text;

    public void SetInfo(ItemState itemState)
    {
        SetItemTitleText(itemState.BaseParams.Title);
        SetItemDescriptionText(itemState.Description);

        foreach (var param in itemState.GetItemParams())
        {
            var parameterView = Instantiate(parameterViewPrefab, parametersSection.transform);
            parameterView.SetInfo(param.Key, param.Value);
        }
    }

    public void SetPosition(Transform linkedItemCell, Vector2 maxOffsetVector)
    {
        Canvas.ForceUpdateCanvases();
        var itemCellPosition = linkedItemCell.position;
        var itemCellRect = linkedItemCell.GetComponent<RectTransform>().rect;
        var viewRect = GetComponent<RectTransform>().rect;

        Vector2 offsetVector;
        if (Screen.width - (itemCellPosition.x + itemCellRect.width / 2) > viewRect.width + maxOffsetVector.x)
        {
            offsetVector = new Vector2(maxOffsetVector.x, itemCellRect.height);
        }
        else if ((itemCellPosition.x - itemCellRect.width / 2) > viewRect.width + maxOffsetVector.x)
        {
            offsetVector = new Vector2(-maxOffsetVector.x, itemCellRect.height);
        }
        else if (Screen.height - (itemCellPosition.y + itemCellRect.height / 2) > viewRect.height + maxOffsetVector.y)
        {
            offsetVector = new Vector2(0, maxOffsetVector.y);
        }
        else
        {
            offsetVector = new Vector2(0, -maxOffsetVector.y);
        }
        transform.position = itemCellPosition + (Vector3)offsetVector;
    }
}
