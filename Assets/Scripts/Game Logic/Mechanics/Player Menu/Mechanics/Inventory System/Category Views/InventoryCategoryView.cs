using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    public abstract class InventoryCategoryView : MonoBehaviour
    {
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}