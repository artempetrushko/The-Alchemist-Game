using Controls;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
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
}