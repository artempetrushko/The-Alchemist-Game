using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMenuSectionNavigation))]
public abstract class PlayerMenuSectionView : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuSectionNavigation sectionNavigation;

    public PlayerMenuSectionNavigation SectionNavigation => sectionNavigation;
}
