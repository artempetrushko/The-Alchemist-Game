using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentConfig : ItemConfig
    {
        [SerializeField] private Sprite _bigIcon;
        [SerializeField] private int _endurance;
    }
}