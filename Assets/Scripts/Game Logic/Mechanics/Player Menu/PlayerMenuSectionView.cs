using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [RequireComponent(typeof(PlayerMenuSectionNavigation))]
    public abstract class PlayerMenuSectionView : MonoBehaviour
    {
        [SerializeField]
        private PlayerMenuSectionNavigation sectionNavigation;

        public PlayerMenuSectionNavigation SectionNavigation => sectionNavigation;
    }
}