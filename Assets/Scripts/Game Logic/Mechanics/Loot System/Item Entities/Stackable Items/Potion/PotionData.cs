using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Potion", menuName = "Game Configs/Items/Potion")]
    public class PotionData : StackableItemConfig
    {
        [Space]
        [SerializeField] private PotionEffect _effect;

        public override ItemState CreateItem() => new PotionState(Id, Icon, PhysicalRepresentation);
    }
}