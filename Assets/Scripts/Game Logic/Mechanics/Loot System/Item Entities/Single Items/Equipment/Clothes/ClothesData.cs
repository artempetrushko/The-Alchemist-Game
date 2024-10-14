using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Game Configs/Items/Equipment/Clothes")]
    public class ClothesData : EquipmentConfig
    {
        [Header("Параметры одежды")]
        [SerializeField] private ClothesType _clothesType;
        [SerializeField] private int _defence;

        public ClothesType ClothesType => _clothesType;
        public int BaseDefence => _defence;

        public override ItemState CreateItem() => new ClothesState(Id, Icon, PhysicalRepresentation);
    }
}