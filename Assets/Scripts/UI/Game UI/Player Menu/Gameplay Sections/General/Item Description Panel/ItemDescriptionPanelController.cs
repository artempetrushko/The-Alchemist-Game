using Cysharp.Threading.Tasks;

public class ItemDescriptionPanelController
{
    private const float PANEL_APPEARANCE_LATENCY_IN_SECONDS = 1f;
    private const float PANEL_POSITION_OFFSET_COEF_X = 0.15f;
    private const float PANEL_POSITION_OFFSET_COEF_Y = 0.3f;

    private ItemDescriptionPanelView _itemDescriptionPanelView;

    public ItemDescriptionPanelController(ItemDescriptionPanelView itemDescriptionPanelView)
    {
        _itemDescriptionPanelView = itemDescriptionPanelView;
    }

    public async UniTask ShowPanel(ItemState item, ItemCellView linkedItemCell)
    {
        //_itemDescriptionPanelView.SetPosition(linkedItemCell.transform, PANEL_POSITION_OFFSET);
        _itemDescriptionPanelView.SetInfo(item);

        await UniTask.WaitForSeconds(PANEL_APPEARANCE_LATENCY_IN_SECONDS);

        _itemDescriptionPanelView.Show();
    }

    public void HidePanel()
    {
        _itemDescriptionPanelView.Hide();
    }
}
