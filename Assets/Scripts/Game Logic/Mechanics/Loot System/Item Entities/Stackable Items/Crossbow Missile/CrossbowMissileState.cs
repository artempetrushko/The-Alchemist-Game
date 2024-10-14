using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;
using UnityEngine;

public class CrossbowMissileState : StackableItemState
{
    public ItemParameter<float> PenetratingPowerCoefficient;

    public CrossbowMissileState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
    {
    }
}
