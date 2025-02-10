using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
	public class AspectsModule : ItemSlotModule
	{
        [SerializeField] private AspectView _aspectViewPrefab;
        [SerializeField] private GameObject _aspectViewsContainer;

        public override void UpdateDisplayedInfo(Item item)
        {
            if (item is Equipment equipment)
            {
                var aspects = equipment.ContainedAspects.Value;
                if (aspects.Count > _aspectViewsContainer.transform.childCount)
                {
                    for (var i = 0; i < aspects.Count - _aspectViewsContainer.transform.childCount; i++)
                    {
                        Instantiate(_aspectViewPrefab, _aspectViewsContainer.transform);
                    }
                }
                else if (aspects.Count < _aspectViewsContainer.transform.childCount)
                {
                    for (var i = aspects.Count - 1; i >= aspects.Count - _aspectViewsContainer.transform.childCount; i--)
                    {
                        var aspectView = _aspectViewsContainer.transform.GetChild(i).GetComponent<AspectView>();
                        aspectView.SetActive(false);
                    }
                }

                for (var i = 0; i < aspects.Count; i++)
                {
                    var aspectView = _aspectViewsContainer.transform.GetChild(i).GetComponent<AspectView>();
                    aspectView.SetInnerAreaImage(aspects[i].Icon);
                    aspectView.SetInnerAreaFillAmount(aspects[i].ProgressionState / aspects[i].MaxProgression);
                }
            } 
            else if (_aspectViewsContainer.transform.childCount > 0)
            {
                foreach (var aspectView in _aspectViewsContainer.GetComponentsInChildren<AspectView>())
                {
                    aspectView.SetActive(false);
                }
            }
        }
    }
}