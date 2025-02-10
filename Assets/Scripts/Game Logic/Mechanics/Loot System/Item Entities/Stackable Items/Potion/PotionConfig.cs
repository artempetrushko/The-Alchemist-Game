using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Potion", menuName = "Game Configs/Items/Potion")]
    public class PotionConfig : StackableItemConfig
    {
        [Space]
        [SerializeField] private PotionEffect _effect;

        public override Item CreateItem() => new Potion(Id, Icon, PhysicalRepresentation);
    }
}