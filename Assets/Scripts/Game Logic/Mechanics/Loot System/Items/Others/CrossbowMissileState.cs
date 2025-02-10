using System;
using System.Collections.Generic;

public class CrossbowMissileState : StackableItemState
{
    public float PenetratingPowerCoefficient { get; set; }

    public CrossbowMissileState(CrossbowMissileData missileData) : base(missileData)
    {

    }

    public override object Clone()
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, string> GetItemParams()
    {
        return new();
    }
}
