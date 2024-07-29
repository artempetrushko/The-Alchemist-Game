using System.Collections;
using System.Collections.Generic;
using UI.PlayerMenu;
using UnityEngine;

[RequireComponent(typeof(PlayerMenuSectionNavigation))]
public abstract class PlayerMenuSectionView : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuSectionNavigation sectionNavigation;

    public PlayerMenuSectionNavigation SectionNavigation => sectionNavigation;
}
