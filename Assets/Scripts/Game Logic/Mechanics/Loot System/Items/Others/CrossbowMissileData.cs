using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Game Entities/Items/Crossbow Missile", order = 51)]
    public class CrossbowMissileData : StackableItemData
    {
        public override ItemState GetItemState() => new CrossbowMissileState(this);
    }
}