using Controls;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public enum PlayerMenuSectionSubsectionType
    {
        Inventory,
        Equipment,
        QuickAccessToolbar,
        Recipes,
        CraftingItemTemplate
    }

    [CreateAssetMenu(fileName = "Player Menu Section Subsection Config", menuName = "Game Configs/Player Menu/Player Menu Section Subsection Config")]
    public class PlayerMenuSectionSubsectionConfig : ScriptableObject
    {
        [SerializeField] private PlayerMenuSectionSubsectionType _type;
        [SerializeField] private PlayerInputActionMap _playerInputActionMap;
        [Space]
        [SerializeField] private PlayerMenuSectionSubsectionConfig _leftNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _rightNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _topNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _bottomNeighboringSubsection;
    }

    public class PlayerMenuSectionConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private PlayerMenuSectionSubsectionConfig[] _subsections;
    }

    public class InventorySectionConfig : PlayerMenuSectionConfig { }

    public class CraftSectionConfig : PlayerMenuSectionConfig { }

    public class AlchemistrySectionConfig : PlayerMenuSectionConfig { }

    public class SettingsSectionConfig : PlayerMenuSectionConfig { }

    public class PlayerMenuConfig : ScriptableObject
    {
        [SerializeField] private PlayerMenuSectionConfig[] _sections;

        public PlayerMenuSectionConfig[] Sections => _sections;
    }
}