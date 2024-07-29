using UnityEngine;

public class ItemCellNavigationModule : MonoBehaviour
{
    [SerializeField] private ItemCellNavigationModule leftNeighboringCell;
    [SerializeField] private ItemCellNavigationModule rightNeighboringCell;
    [SerializeField] private ItemCellNavigationModule topNeighboringCell;
    [SerializeField] private ItemCellNavigationModule bottomNeighboringCell;

    public ItemCellNavigationModule LeftNeighboringCell => leftNeighboringCell;
    public ItemCellNavigationModule RightNeighboringCell => rightNeighboringCell;
    public ItemCellNavigationModule TopNeighboringCell => topNeighboringCell;
    public ItemCellNavigationModule BottomNeighboringCell => bottomNeighboringCell;
}
