using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Game UI Config", menuName = "Game Data/UI/Game UI Config")]
    public class GameUIConfig : ScriptableObject
    {
        [SerializeField]
        private float _itemDescriptionAppearanceLatencyInSeconds = 1f;

        public float ItemDescriptionAppearanceLatencyInSeconds => _itemDescriptionAppearanceLatencyInSeconds;
    }
}
