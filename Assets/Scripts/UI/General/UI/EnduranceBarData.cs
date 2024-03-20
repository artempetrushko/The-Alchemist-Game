using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Endurance Bar Data", menuName = "UI/Item Cell/Endurance Bar Data", order = 51)]
public class EnduranceBarData : ScriptableObject
{
    [SerializeField]
    private List<EnduranceBarState> enduranceBarStates = new();

    public Color GetEnduranceBarColor(float barFillAmount)
    {
        foreach (var state in enduranceBarStates)
        {
            if (barFillAmount >= state.BarStateMinPercentage
                && barFillAmount <= state.BarStateMaxPercentage)
            {
                return state.BarColor;
            }
        }
        return enduranceBarStates[0].BarColor;
    }    
}
