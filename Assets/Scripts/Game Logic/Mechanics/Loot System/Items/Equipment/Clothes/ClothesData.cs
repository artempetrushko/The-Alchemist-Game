using UnityEngine;

namespace GameLogic.LootSystem
{

    public enum ClothesType
    {
        Hat,
        Raincoat,
        Boots,
        Gloves,
        Medallion
    }

    [CreateAssetMenu(fileName = "New Clothes", menuName = "Game Entities/Items/Equipment/Clothes", order = 51)]
    public class ClothesData : EquipmentData
    {
        [Header("Параметры одежды")]
        [SerializeField]
        protected ClothesType clothesType;
        [SerializeField]
        protected int defence;

        public ClothesType ClothesType => clothesType;
        public int BaseDefence => defence;

        public override ItemState GetItemState() => new ClothesState(this);
    }
}
