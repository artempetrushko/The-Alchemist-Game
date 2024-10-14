using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;
using UnityEngine;

public class CrossbowState : RangedWeaponState
{
    public CrossbowState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
    {
    }
}
