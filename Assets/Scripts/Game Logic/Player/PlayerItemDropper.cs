using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerItemDropper : MonoBehaviour
    {
        [SerializeField] private Transform _droppedItemsSpawnPoint;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ItemDroppedSignal>(OnItemDropped);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ItemDroppedSignal>(OnItemDropped);
        }

        private void OnItemDropped(ItemDroppedSignal signal)
        {
            var droppedItem = Instantiate(signal.Item.PhysicalRepresentation, _droppedItemsSpawnPoint.transform);
            droppedItem.ItemState = signal.Item;
        }
    }
}