using UnityEngine;

namespace Controls
{
    public class ControlsTipsSectionView : MonoBehaviour
    {
        [SerializeField] private DetailedControlTipView _detailedControlTipViewPrefab;
        [SerializeField] private GameObject _detailedControlTipViewsContainer;

        public GameObject DetailedControlTipViewsContainer => _detailedControlTipViewsContainer;
    }
}