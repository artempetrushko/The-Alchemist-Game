using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionsManager : MonoBehaviour
{
    [SerializeField]
    private ItemDescriptionView itemDescriptionViewPrefab;
    [SerializeField]
    private GameObject itemDescriptionViewContainer;
    [SerializeField]
    private float viewAppearanceLatencyInSeconds = 1f;

    private ItemDescriptionView currentItemDescriptionView;
    private bool isViewCreationAvailable;
    private Vector2 viewPositionOffsetVector = new(Screen.width * 0.15f, Screen.height * 0.3f);

    public void CreateItemDescriptionView(ItemState item, ItemCellView linkedItemCell) => StartCoroutine(CreateItemDescriptionView_COR(item, linkedItemCell));

    public void ClearItemDescriptionView()
    {
        isViewCreationAvailable = false;
        DestroyItemDescriptionView();
    }

    private IEnumerator CreateItemDescriptionView_COR(ItemState item, ItemCellView linkedItemCell)
    {
        isViewCreationAvailable = true;
        DestroyItemDescriptionView();
        currentItemDescriptionView = Instantiate(itemDescriptionViewPrefab, itemDescriptionViewContainer.transform);
        currentItemDescriptionView.SetInfo(item);
        currentItemDescriptionView.SetPosition(linkedItemCell.transform, viewPositionOffsetVector);

        yield return new WaitForSecondsRealtime(viewAppearanceLatencyInSeconds);
        if (isViewCreationAvailable)
        {
            currentItemDescriptionView.Show();            
        }      
    }

    private void DestroyItemDescriptionView()
    {
        if (currentItemDescriptionView != null)
        {
            Destroy(currentItemDescriptionView.gameObject);
        }
    }
}
