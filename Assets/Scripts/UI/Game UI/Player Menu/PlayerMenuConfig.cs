using Controls;
using UnityEngine;

namespace UI.PlayerMenu
{
    public enum PlayerMenuSectionType
    {
        Inventory,
        Craft,
        Alchemistry,
        Settings
    }

    public enum PlayerMenuSectionSubsectionType
    {
        Inventory,
        Equipment,
        QuickAccessToolbar,
        Recipes,
        CraftingItemTemplate
    }

    [CreateAssetMenu(fileName = "Player Menu Section Subsection Config", menuName = "Game Data/Player Menu/Player Menu Section Subsection Config")]
    public class PlayerMenuSectionSubsectionConfig : ScriptableObject
    {
        [SerializeField] private PlayerMenuSectionSubsectionType _type;
        [SerializeField] private GameObject _subsectionViewPrefab;
        [SerializeField] private PlayerInputActionMap _playerInputActionMap;
        [Space]
        [SerializeField] private PlayerMenuSectionSubsectionConfig _leftNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _rightNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _topNeighboringSubsection;
        [SerializeField] private PlayerMenuSectionSubsectionConfig _bottomNeighboringSubsection;
    }

    public class PlayerMenuSectionConfig : ScriptableObject
    {
        [SerializeField] private PlayerMenuSectionType _type;
        [SerializeField] private GameObject _sectionViewPrefab;
        [SerializeField] private PlayerMenuSectionSubsectionConfig[] _subsections;
    }

    public class PlayerMenuConfig : ScriptableObject
    {

    }
}