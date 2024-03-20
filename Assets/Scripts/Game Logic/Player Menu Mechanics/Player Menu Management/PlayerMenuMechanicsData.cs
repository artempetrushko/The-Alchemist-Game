using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMenuMechanicsData
{
    [SerializeField]
    private PlayerMenuMechanicsManager mechanicsManager;
    [SerializeField]
    private PlayerMenuSectionView mechanicsViewPrefab;
    [SerializeField]
    private Sprite sectionIcon;

    public PlayerMenuMechanicsManager MechanicsManager => mechanicsManager;
    public PlayerMenuSectionView MechanicsViewPrefab => mechanicsViewPrefab;
    public Sprite SectionIcon => sectionIcon;
}
