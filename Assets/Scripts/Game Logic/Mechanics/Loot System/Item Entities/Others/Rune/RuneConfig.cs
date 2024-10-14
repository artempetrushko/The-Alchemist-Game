using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Rune", menuName = "Game Configs/Items/Rune")]
    public class RuneConfig : ScriptableObject
    {
        [SerializeField] protected AspectConfig[] _sealedAspects;
        [SerializeField] protected ItemEffect _resultEffect;

        public AspectConfig[] SealedAspects => _sealedAspects;
        public ItemEffect ResultEffect => _resultEffect;
    }
}