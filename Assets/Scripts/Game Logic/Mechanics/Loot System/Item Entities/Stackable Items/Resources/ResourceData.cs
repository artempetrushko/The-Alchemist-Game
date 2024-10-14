using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class ResourceData : StackableItemConfig
    {
        [SerializeField] protected ItemRarity _rarity;
        [SerializeField] protected CharacteristicCoeffsSet coefficientsSet;
        [SerializeField] protected ItemEffect[] _effects;

        public ItemRarity Rarity => _rarity;
        public ItemEffect[] Features => _effects;
        public CharacteristicCoeffsSet CoefficientsSet => coefficientsSet;
    }
}