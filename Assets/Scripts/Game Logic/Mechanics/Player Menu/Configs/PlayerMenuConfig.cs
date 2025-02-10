using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [CreateAssetMenu(fileName = "Player Menu Config", menuName = "Game Configs/Player Menu/Player Menu Config")]
    public class PlayerMenuConfig : ScriptableObject
    {
        [SerializeField] private PlayerMenuSectionConfig[] _sections;

        public PlayerMenuSectionConfig[] Sections => _sections;
    }
}