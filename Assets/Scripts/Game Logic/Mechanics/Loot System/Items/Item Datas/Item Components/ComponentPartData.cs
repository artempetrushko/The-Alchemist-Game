using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentPartType
{
    All,
    SwordBlade,
    SwordHandle,
    KnifeBlade,
    KnifeHandle,
    StaveRod,
    StavePommel,
    MissileTip,
    MissileRod
}

[CreateAssetMenu(fileName = "New Component Part", menuName = "Game Entities/Items/Component Part", order = 51)]
public class ComponentPartData : ScriptableObject
{
    [SerializeField]
    private ComponentPartType type;
    [SerializeField]
    private string title;
    [SerializeField]
    protected MaterialData material;
    [SerializeField]
    protected List<MaterialData> materialsToCraft = new();
    [SerializeField]
    protected CharacteristicCoeffsSet coeffsSet;

    public ComponentPartType Type => type;
    public string Title => title;
    public MaterialData BaseMaterial => material;
    public List<MaterialData> MaterialsToCraft => materialsToCraft;
    public CharacteristicCoeffsSet CoeffsSet => coeffsSet;
}
