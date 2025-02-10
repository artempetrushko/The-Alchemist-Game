using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySectionView : PlayerMenuSectionView
{
    [SerializeField]
    private InventorySubsectionView inventorySubsectionView;

    public InventorySubsectionView InventorySubsectionView => inventorySubsectionView;
}
