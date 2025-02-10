using System.Collections.Generic;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.Player
{
    public class PlayerItemPicker : MonoBehaviour
    {
        public List<PickableItem> PickableItems { get; private set; } = new();

        private void OnTriggerEnter(Collider other)
        {
            var pickableItem = other.GetComponent<PickableItem>();
            if (pickableItem != null && !PickableItems.Contains(pickableItem))
            {
                PickableItems.Add(pickableItem);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PickableItems.Remove(other.GetComponent<PickableItem>());
        }
    }
}