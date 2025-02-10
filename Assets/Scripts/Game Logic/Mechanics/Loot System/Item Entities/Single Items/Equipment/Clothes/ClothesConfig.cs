using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Game Configs/Items/Equipment/Clothes")]
    public class ClothesConfig : EquipmentConfig
    {
        [Header("Параметры одежды")]
        [SerializeField] private ClothesType _clothesType;
        [SerializeField] private int _defence;

        public ClothesType ClothesType => _clothesType;
        public int BaseDefence => _defence;

        public override Item CreateItem() => new Clothes(Id, Icon, PhysicalRepresentation);
    }
}