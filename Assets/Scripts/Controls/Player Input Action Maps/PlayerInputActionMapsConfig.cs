using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(fileName = "Player Input Action Maps Config", menuName = "Game Data/Controls/Player Input Action Maps Config")]
    public class PlayerInputActionMapsConfig : ScriptableObject
    {
        [SerializeField] private PlayerInputActionMapData[] _actionMapDatas;

        public PlayerInputActionMapData[] ActionMapDatas => _actionMapDatas;
    }
}
