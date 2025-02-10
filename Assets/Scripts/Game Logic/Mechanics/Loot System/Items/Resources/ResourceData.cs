using UnityEngine;

namespace GameLogic.LootSystem
{
    public enum RareClass
    {
        Junk,
        Ordinary,
        Rare,
        Precious
    }

    public abstract class ResourceData : StackableItemData
    {
        [Header("Параметры ресурса")]
        [SerializeField]
        protected RareClass rare;
        [SerializeField]
        protected CharacteristicCoeffsSet coefficientsSet;
        [SerializeField]
        protected bool isShredded;

        public RareClass Rare => rare;
        public CharacteristicCoeffsSet CoefficientsSet => coefficientsSet;
        public bool IsShredded => isShredded;
    }
}
