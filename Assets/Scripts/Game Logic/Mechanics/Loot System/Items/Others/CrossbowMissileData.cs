using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Game Entities/Items/Crossbow Missile", order = 51)]
public class CrossbowMissileData : StackableItemData
{
    [Header("��������� ����������� �������")]
    [SerializeField]
    protected List<RequiredComponentPart> componentParts = new();

    public List<RequiredComponentPart> BaseComponentParts => componentParts;

    public override ItemState GetItemState() => new CrossbowMissileState(this);
}
