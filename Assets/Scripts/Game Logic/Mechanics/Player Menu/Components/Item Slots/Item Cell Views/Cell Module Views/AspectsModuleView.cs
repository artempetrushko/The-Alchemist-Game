using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class AspectsModuleView : ItemCellModuleView
    {
        [SerializeField]
        private AspectSlotView aspectViewPrefab;
        [SerializeField]
        private GameObject aspectViewsContainer;

        public override void SetActive(bool isActive)
        {
            aspectViewsContainer.SetActive(isActive);
        }

        public override bool TryEnableWithNewItem(ItemState newItem)
        {
            return false;
        }

        public override void UpdateContent(ItemState attachedItem)
        {
            ClearAspectViews();

            var aspects = attachedItem.Aspects;
            for (var i = 0; i < aspects.Count; i++)
            {
                var aspectIcon = Instantiate(aspectViewPrefab, transform);
                aspectIcon.SetAppearance(aspects[i].Icon, aspects[i].ProgressionState / aspects[i].MaxProgression);
            }
        }

        private void ClearAspectViews()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}