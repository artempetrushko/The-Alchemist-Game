using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "resource", menuName = "Game Configs/Items/Resource")]
    public class ResourceConfig : StackableItemConfig
    {
        [SerializeField] private ItemRarity _rarity;
        [SerializeField] private CharacteristicCoeffsSet _coefficientsSet;
        [SerializeField] private ItemEffect[] _effects;

        public override Item CreateItem()
        {
            throw new System.NotImplementedException();
        }
    }
}