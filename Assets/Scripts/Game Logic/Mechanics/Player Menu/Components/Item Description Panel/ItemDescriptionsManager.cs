using System.Collections;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemDescriptionsManager : MonoBehaviour
    {
        [SerializeField]
        private ItemDescriptionView itemDescriptionView;
        [SerializeField]
        private float viewAppearanceLatencyInSeconds = 1f;

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
            itemDescriptionView.gameObject.SetActive(true);
            itemDescriptionView.SetInfo(item);
            itemDescriptionView.SetPosition(linkedItemCell.transform, viewPositionOffsetVector);

            yield return new WaitForSecondsRealtime(viewAppearanceLatencyInSeconds);
            if (isViewCreationAvailable)
            {
                itemDescriptionView.Show();
            }
        }

        private void DestroyItemDescriptionView()
        {
            if (itemDescriptionView != null)
            {
                Destroy(itemDescriptionView.gameObject);
            }
        }
    }
}