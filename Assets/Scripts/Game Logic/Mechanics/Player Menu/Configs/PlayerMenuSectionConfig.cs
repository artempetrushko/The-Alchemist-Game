using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class PlayerMenuSectionConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private PlayerMenuSectionSubsectionConfig[] _subsections;
    }
}