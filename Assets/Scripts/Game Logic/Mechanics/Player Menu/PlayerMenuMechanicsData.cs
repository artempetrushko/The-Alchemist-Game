using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class PlayerMenuMechanicsData
    {
        [SerializeField]
        private PlayerMenuMechanicsManager mechanicsManager;
        [SerializeField]
        private PlayerMenuSectionView mechanicsViewPrefab;
        [SerializeField]
        private Sprite sectionIcon;

        public PlayerMenuMechanicsManager MechanicsManager => mechanicsManager;
        public PlayerMenuSectionView MechanicsViewPrefab => mechanicsViewPrefab;
        public Sprite SectionIcon => sectionIcon;
    }
}