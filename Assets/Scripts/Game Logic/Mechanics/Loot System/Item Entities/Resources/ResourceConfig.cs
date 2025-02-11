using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "resource", menuName = "Game Configs/Items/Resource")]
    public class ResourceConfig : StackableItemConfig
    {
        [SerializeField] private ItemRarity _rarity;

        public override Item CreateItem()
        {
            throw new System.NotImplementedException();
        }
    }
}